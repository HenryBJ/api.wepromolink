using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO.BTCPay;
using WePromoLink.Models;
using WePromoLink.Settings;
using static BTCPayServer.Client.Models.InvoiceDataBase;

namespace WePromoLink.Services;

public class BTCPaymentService : IPaymentService
{
    private readonly BTCPayServerClient _client;
    private readonly IOptions<BTCPaySettings> _settings;
    private readonly ILogger<BTCPaymentService> _logger;
    private readonly DataContext _bd;

    public BTCPaymentService(ILogger<BTCPaymentService> logger, IOptions<BTCPaySettings> settings, BTCPayServerClient client, DataContext bd)
    {
        _logger = logger;
        _settings = settings;
        _client = client;
        _bd = bd;
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
            //  _logger.LogInformation($"Received BTCPay event: {event_type}"); 
            try
            {
                Type? type = Type.GetType($"WePromoLink.DTO.BTCPay.{event_type}");
                if (type == null) throw new Exception("BTCPay event invalid");
                var btcpayEvent = data.Deserialize(type);
                await ProcessEvent(btcpayEvent);
                // await _mediator.Publish(new WebHookNotification{Event = btcpayEvent as BTCPayEventBase});
            }
            catch (System.Exception ex)
            {
                _logger.LogWarning($"Parsing {event_type} error: {ex.Message}");
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
                var pay = invoiceData.Metadata.ToObject<PaymentTransaction>();
                _logger.LogInformation($"Amount:{invoiceData.Amount} PaymentId:{pay!.Id} ");
                await ProcessPayment(invoiceData, pay);
                break;
            default:
                _logger.LogInformation(btcpayEvent?.ToString());
                break;
        }
    }

    private async Task ProcessPayment(InvoiceData invoiceData, PaymentTransaction pay)
    {
        // try
        // {
        //     var fee = _settings.Value.Fee;
        //     var commition = invoiceData.Amount * fee;
        //     var remaining = invoiceData.Amount - commition;
        //     using var transaction = await _bd.Database.BeginTransactionAsync();

        //     pay.Amount = remaining;
        //     pay.CompletedAt = DateTime.UtcNow;
        //     pay.Status = "COMPLETED";

        //     PaymentTransaction feepay = new PaymentTransaction
        //     {
        //         Title = "FEE",
        //         Amount = commition,
        //         Status = "PENDING",
        //         CreatedAt = DateTime.UtcNow,
        //         IsDeposit = false,
        //         EmailModelId = pay.EmailModelId,
        //         SponsoredLinkId = pay.SponsoredLinkId,
        //         AffiliateLinkId = pay.AffiliateLinkId
        //     };

        //     _bd.PaymentTransactions.Update(pay);
        //     await _bd.PaymentTransactions.AddAsync(feepay);
        //     await _bd.SaveChangesAsync();
        //     await transaction.CommitAsync();
        //     // TODO: here add notification

        // }
        // catch (System.Exception ex)
        // {

        //     throw;
        // }

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

    public async Task<string> CreateInvoice(PaymentTransaction payment, string RedirectUrl = "")
    {
        var result = await _client.CreateInvoice(_settings.Value.StoreId, new CreateInvoiceRequest
        {
            Amount = payment.Amount,
            Currency = "BTC",
            Checkout = new CheckoutOptions
            {
                SpeedPolicy = SpeedPolicy.MediumSpeed,
                Expiration = TimeSpan.FromHours(5),
                RedirectURL = RedirectUrl
            },
            Metadata = JObject.FromObject(payment)
        });

        return result.CheckoutLink;
    }
}