using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO.BTCPay;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Settings;
using static BTCPayServer.Client.Models.InvoiceDataBase;

namespace WePromoLink.Services;

public class BTCPaymentService
{
    private readonly BTCPayServerClient _client;
    private readonly IOptions<BTCPaySettings> _settings;
    private readonly ILogger<BTCPaymentService> _logger;
    private readonly DataContext _bd;
    private readonly IUserService _userService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BTCPaymentService(ILogger<BTCPaymentService> logger, IOptions<BTCPaySettings> settings, BTCPayServerClient client, DataContext bd, IUserService userService, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _settings = settings;
        _client = client;
        _bd = bd;
        _userService = userService;
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
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
                var payment = _bd.PaymentTransactions.Where(e => e.Id == paymentId).Single();
                await ProcessPayment(invoiceData!, payment);
                break;
            default:
                _logger.LogInformation(btcpayEvent?.ToString());
                break;
        }
    }

    private async Task ProcessPayment(InvoiceData invoiceData, PaymentTransaction pay)
    {
        await _userService.Deposit(pay);
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
        using (var transaction = _bd.Database.BeginTransaction())
        {
            try
            {
                var userId = _bd.Users
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
                _bd.PaymentTransactions.Add(payment);
                await _bd.SaveChangesAsync();

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
        var user = _bd.Users
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
            _bd.BitcoinBillings.Update(billingInfo);
            await _bd.SaveChangesAsync();
            return true;

        }
        catch (System.Exception)
        {
            return false;
        }
    }

}