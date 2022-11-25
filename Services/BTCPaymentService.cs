using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class BTCPaymentService : IPaymentService
{
    private readonly IOptions<BTCPaySettings> _settings;
    private readonly ILogger<BTCPaymentService> _logger;

    public BTCPaymentService(ILogger<BTCPaymentService> logger, IOptions<BTCPaySettings> settings)
    {
        _logger = logger;
        _settings = settings;
    }

    public async Task HandleWebHook(HttpContext ctx)
    {
        ctx.Request.Headers.TryGetValue("BTCPAY-SIG",out var btcpay_sig);       
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
                Type? type = Type.GetType($"WePromoLink.DTO.BTCPay.{event_type}");
                if(type == null) throw new Exception("BTCPay event invalid");
                var btcpayEvent = data.Deserialize(type);
                _logger.LogInformation(btcpayEvent?.ToString());
                // await _mediator.Publish(new WebHookNotification{Event = btcpayEvent as BTCPayEventBase});
             }
             catch (System.Exception ex)
             {
                _logger.LogWarning($"Parsing {event_type} error: {ex.Message}");
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
            if(btcpay_sig != $"sha256={cad.ToLower()}")
            {
                _logger.LogWarning("WebHook Invalid");
                return false;
            }
            return true;
        }
    }

    private string getEventType(JsonElement data)
    {
        if(data.ValueKind == JsonValueKind.Object)
        {
            foreach (var item in data.EnumerateObject())
            {
                if(item.Name == "type")
                {
                    if(item.Value.ValueKind == JsonValueKind.String)
                    {
                        string result = item.Value.Deserialize<string>();
                        return result;
                    }
                }                
            }
        }
        return null;
    }
}