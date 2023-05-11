using Microsoft.EntityFrameworkCore;
using Stripe;
using WePromoLink.Data;
using WePromoLink.Models;
using WePromoLink.Services;

namespace WePromoLink.Workers;

public class WebHookWorker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 2;
    private readonly WebHookEventQueue _queue;
    private readonly ILogger<WebHookWorker> _logger;
    private readonly StripeService _stripeService;

    public WebHookWorker(WebHookEventQueue queue, IServiceScopeFactory fac, ILogger<WebHookWorker> logger)
    {
        var scope = fac.CreateScope();
        _stripeService = scope.ServiceProvider.GetRequiredService<StripeService>();
        _queue = queue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var item = _queue.Item;
            if (item != null)
            {
                await ProcessEvent(item);
            }
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS));
        }
    }

    private async Task ProcessEvent(Event item)
    {
        try
        {
            switch (item.Type)
            {
                case Events.CustomerSubscriptionCreated:
                    await _stripeService.CreateUser(item.Data.Object as Subscription);
                    break;

                case Events.CustomerSubscriptionDeleted:
                case Events.CustomerSubscriptionPaused:
                case Events.CustomerSubscriptionPendingUpdateApplied:
                case Events.CustomerSubscriptionPendingUpdateExpired:
                case Events.CustomerSubscriptionResumed:
                case Events.CustomerSubscriptionTrialWillEnd:
                case Events.CustomerSubscriptionUpdated:
                    var sub = item.Data.Object as Subscription;
                    await _stripeService.UpdateUserSubscription(sub!, sub!.Status);
                    break;

                default:
                    Console.WriteLine("Unhandled event type: {0}", item.Type);
                    break;
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

}