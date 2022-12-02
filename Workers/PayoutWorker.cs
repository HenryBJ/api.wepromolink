using BTCPayServer.Client;
using BTCPayServer.Client.Models;
using Microsoft.Extensions.Options;
using WePromoLink.Data;
using WePromoLink.Settings;

namespace WePromoLink.Workers;

public class PayoutWorker : BackgroundService
{
    const int WAIT_TIME_HOURS = 20;
    private readonly BTCPayServerClient _client;
    private readonly DataContext _db;
    private readonly IOptions<BTCPaySettings> _settings;
    private readonly ILogger<PayoutWorker> _logger;


    public PayoutWorker(IServiceScopeFactory fac, ILogger<PayoutWorker> logger)
    {
        using var scope = fac.CreateScope();
        _client = scope.ServiceProvider.GetRequiredService<BTCPayServerClient>();
        _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            // var pullpayments = await _client.GetPullPayments(_settings.Value.StoreId);
            // if(pullpayments == null || pullpayments.Length == 0) continue;
            
            
            // var affiliateLinks = _db.AffiliateLinks.Where(e=>e.Available > e.Threshold)

            await Task.Delay(TimeSpan.FromHours(WAIT_TIME_HOURS));
        }
    }
}