using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO.BTCPay;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Settings;
using WePromoLink.Shared.RabbitMQ;
using static BTCPayServer.Client.Models.InvoiceDataBase;

namespace WePromoLink.Services;

public class BTCPaymentService
{
    private readonly BTCPayServerClient _client;
    private readonly IOptions<BTCPaySettings> _settings;
    private readonly ILogger<BTCPaymentService> _logger;
    private readonly DataContext _db;
    private readonly IUserService _userService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly MessageBroker<BaseEvent> _senderEvent;

    public BTCPaymentService(ILogger<BTCPaymentService> logger, IOptions<BTCPaySettings> settings, BTCPayServerClient client, DataContext bd, IUserService userService, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, MessageBroker<BaseEvent> senderEvent)
    {
        _logger = logger;
        _settings = settings;
        _client = client;
        _db = bd;
        _userService = userService;
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
        _senderEvent = senderEvent;
    }

    public async Task HandleWebHook(HttpContext ctx)
    {
        ctx.Request.Headers.TryGetValue("BTCPAY-SIG", out var btcpay_sig);
        ctx.Request.EnableBuffering();

        if (await VerifyEvent(btcpay_sig, ctx.Request.Body))
        {
            ctx.Request.Body.Position = 0;
            var json = await new StreamReader(ctx.Request.Body).ReadToEndAsync();

            JsonElement data = JsonSerializer.Deserialize<JsonElement>(json);
            string event_type = getEventType(data);
            _logger.LogInformation($"Received BTCPay event: {event_type}");
            try
            {
                string path = $"WePromoLink.DTO.BTCPay.{event_type}";
                _logger.LogInformation(path);
                Type? type = Type.GetType(path);
                if (type == null) throw new Exception("BTCPay event invalid");
                var btcpayEvent = data.Deserialize(type);
                await ProcessEvent(btcpayEvent);
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning($"Parsing {event_type} error: {ex.Message}");
                throw;
            }
        }
    }

    private async Task ProcessEvent(object? btcpayEvent)
    {
        if (btcpayEvent == null) return;
        switch (btcpayEvent!)
        {
            case InvoiceSettled ev:
                var invoiceData = await _client.GetInvoice(_settings.Value.StoreId, ev.InvoiceId);
                var paymentId = invoiceData?.Metadata["paymentId"]?.ToObject<Guid>();
                var payment = _db.PaymentTransactions.Include(e=>e.User).Where(e => e.Id == paymentId).Single();
                await ProcessPayment(invoiceData!, payment);
                break;

            case InvoiceExpired eve:
                var invoiceDataFail = await _client.GetInvoice(_settings.Value.StoreId, eve.InvoiceId);
                ProcessFailInvoice(invoiceDataFail, nameof(InvoiceExpired));
                break;

            case InvoiceInvalid evf:
                var invoiceDataFail2 = await _client.GetInvoice(_settings.Value.StoreId, evf.InvoiceId);
                ProcessFailInvoice(invoiceDataFail2, nameof(InvoiceInvalid));
                break;

            default:
                _logger.LogInformation(btcpayEvent?.ToString());
                break;
        }
    }

    private void ProcessFailInvoice(InvoiceData invoiceDataFail, string reason)
    {
        var paymentId = invoiceDataFail?.Metadata["paymentId"]?.ToObject<Guid>();
        var payment = _db.PaymentTransactions.Include(e => e.User).Where(e => e.Id == paymentId).Single();
        _senderEvent.Send(new DepositFailureEvent
        {
            PaymentMethod = "Bitcoin",
            Amount = invoiceDataFail?.Amount ?? 0,
            FailureReason = reason,
            Name = payment.User?.Fullname,
            PaymentTransactionId = payment.Id,
            UserId = payment.User?.Id ?? Guid.Empty
        });
    }

    private async Task ProcessPayment(InvoiceData invoiceData, PaymentTransaction pay)
    {
        await _userService.Deposit(pay);
    }

    public async Task CreateWithdrawRequest(int amount)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                int amountCents = amount * 100;
                // Get User
                var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
                var user = await _db.Users
                .Include(e => e.Profit)
                .Where(e => e.FirebaseId == firebaseId)
                .SingleAsync();

                // Discount from Profit
                user.Profit.Value -= amount;
                user.Profit.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                _db.Profits.Update(user.Profit);

                if (user.Profit.Value < 0) throw new Exception("Profit negative");

                // Create Payment Transaction
                var paymentTrans = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = TransactionStatusEnum.Requesting,
                    Title = $"Withdraw ${amount}",
                    TransactionType = TransactionTypeEnum.Withdraw,
                    UserModelId = user.Id,
                    ExpiredAt = DateTime.UtcNow.AddDays(10),
                    Metadata = "bitcoin"
                };
                await _db.PaymentTransactions.AddAsync(paymentTrans);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Creating payment transaction fail");
            }
        }
    }

    private async Task<bool> VerifyEvent(StringValues btcpay_sig, Stream stream)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_settings.Value.Secret)))
        {
            var result = await hmac.ComputeHashAsync(stream);
            var cad = Convert.ToHexString(result);
            // Console.WriteLine($"HASH: sha256={cad.ToLower()}");
            if (btcpay_sig != $"sha256={cad.ToLower()}")
            {
                _logger.LogWarning("WebHook Invalid");
                return false;
            }
            return true;
        }
    }

    private string getEventType(JsonElement data)
    {
        if (data.ValueKind == JsonValueKind.Object)
        {
            foreach (var item in data.EnumerateObject())
            {
                if (item.Name == "type")
                {
                    if (item.Value.ValueKind == JsonValueKind.String)
                    {
                        string result = item.Value.Deserialize<string>();
                        return result;
                    }
                }
            }
        }
        return null;
    }

    public async Task<string> CreateInvoice(decimal amount, string firebaseId)
    {
        BTCPayServer.Client.Models.InvoiceData? result = null;
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                var userId = _db.Users
                .Where(e => e.FirebaseId == firebaseId)
                .Where(e => e.IsSubscribed)
                .Where(e => !e.IsBlocked)
                .Select(e => e.Id)
                .Single();

                PaymentTransaction payment = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    UserModelId = userId,
                    CreatedAt = DateTime.UtcNow,
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = TransactionStatusEnum.Pending,
                    TransactionType = TransactionTypeEnum.Deposit,
                    Title = $"Deposit ${amount}",
                    ExpiredAt = DateTime.UtcNow.AddHours(5)
                };
                _db.PaymentTransactions.Add(payment);
                await _db.SaveChangesAsync();

                result = await _client.CreateInvoice(_settings.Value.StoreId, new CreateInvoiceRequest
                {
                    Amount = amount,
                    Currency = "USD",
                    Checkout = new CheckoutOptions
                    {
                        SpeedPolicy = SpeedPolicy.MediumSpeed,
                        Expiration = TimeSpan.FromHours(5),
                        RedirectURL = "https://wepromolink.com/balance"
                    },
                    Metadata = JObject.FromObject(new { paymentId = payment.Id })
                });
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Creating invoice fail");
            }
        }
        return result?.CheckoutLink ?? "";
    }

    public async Task<bool> VerifyAddress(string address)
    {
        const string URL = "https://blockchain.info/rawaddr/{0}";
        HttpClient httpClient = _httpClientFactory.CreateClient();

        // Get UserId
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = _db.Users
        .Include(e => e.BitcoinBillingMethod)
        .Where(e => e.FirebaseId == firebaseId).Single();

        try
        {
            var requestUrl = string.Format(URL, address);
            var response = await httpClient.GetFromJsonAsync<BitcoinAddressData>(requestUrl);
            var isValid = response.total_received > 0 || response.total_sent > 0;
            if (!isValid) return false;
            var billingInfo = user.BitcoinBillingMethod;
            billingInfo.IsVerified = true;
            billingInfo.LastModified = DateTime.UtcNow;
            billingInfo.VerifiedAt = DateTime.UtcNow;
            billingInfo.Address = address;
            _db.BitcoinBillings.Update(billingInfo);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (System.Exception)
        {
            return false;
        }
    }

}