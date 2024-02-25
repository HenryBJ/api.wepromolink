using Microsoft.EntityFrameworkCore;
using Stripe;
using WePromoLink.Data;
using WePromoLink.Services;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.StripeWorker;

public class CommissionWorker : BackgroundService
{
    const int WAIT_TIME_MIN = 1;
    const int HIT_BULK = 300;
    private readonly ILogger<CommissionWorker> _logger;
    private readonly DataContext _db;

    public CommissionWorker(ILogger<CommissionWorker> logger, IServiceScopeFactory fac)
    {
        var scope = fac.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await ProcessHitCommission();
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromMinutes(WAIT_TIME_MIN), stoppingToken);
        }
    }

    private async Task ProcessHitCommission()
    {
        try
        {
            var list = await _db.Hits.Where(e => !e.Payed && e.Commission > 0).Take(HIT_BULK).ToListAsync();
            var group_list = list.GroupBy(e => e.SubscriptionId).Select(g => new
            {
                SubscriptionId = g.Key,
                Items = g.ToList(),
                Quantity = g.Count()
            }).ToList();

            foreach (var hitGroup in group_list)
            {
                // Obtener la suscripción de Stripe
                var subscription = await GetSubscriptionFromStripe(hitGroup.SubscriptionId);
                if (subscription == null)
                {
                    _logger.LogError("Subscription not found");
                    continue;
                }

                // Obtener los Subscription Items de la suscripción
                var subscriptionItems = await GetSubscriptionItemsFromStripe(subscription.Id);
                if (subscriptionItems == null || !subscriptionItems.Any())
                {
                    _logger.LogError("Subscription items not found");
                    continue;
                }

                // Obtener el primer Subscription Item
                var matchingSubscriptionItem = subscriptionItems.FirstOrDefault();
                if (matchingSubscriptionItem == null)
                {
                    _logger.LogError("Subscription item not found");
                    continue;
                }

                // Actualizar el uso medido utilizando UsageRecordService
                await UpdateUsageRecord(matchingSubscriptionItem.Id, hitGroup.Quantity);

                // Actualizar las entidades HitModel con Payed = true
                foreach (var hit in hitGroup.Items)
                {
                    hit.Payed = true;
                    _db.Entry(hit).State = EntityState.Modified;
                }
                await _db.SaveChangesAsync();
            }

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    public async Task<Subscription> GetSubscriptionFromStripe(string subscriptionId)
    {
        var service = new SubscriptionService();
        return await service.GetAsync(subscriptionId);
    }

    public async Task<IEnumerable<SubscriptionItem>> GetSubscriptionItemsFromStripe(string subscriptionId)
    {
        var options = new SubscriptionItemListOptions
        {
            Subscription = subscriptionId,
        };

        var service = new SubscriptionItemService();
        var subscriptionItems = await service.ListAsync(options);

        return subscriptionItems;
    }

    public async Task UpdateUsageRecord(string subscriptionItemId, int quantity)
    {
        var options = new UsageRecordCreateOptions
        {
            Quantity = quantity,
        };
        var service = new UsageRecordService();
        await service.CreateAsync(subscriptionItemId, options);

        _logger.LogInformation($"UsageRecord: {subscriptionItemId}->({quantity}) Created");
    }
}
