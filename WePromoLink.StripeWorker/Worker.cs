using Stripe;
using WePromoLink.Services;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.StripeWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 2;
    private readonly StripeService _stripeService;
    private readonly ILogger<Worker> _logger;
    private MessageBroker<Event> _messageBroker;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory fac)
    {
        var scope = fac.CreateScope();
        _stripeService = scope.ServiceProvider.GetRequiredService<StripeService>();
        _messageBroker = scope.ServiceProvider.GetRequiredService<MessageBroker<Event>>();
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _messageBroker.Receive((ev) => ProcessEvent(ev).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> ProcessEvent(Event item)
    {
        // Hay que asegurarse que todos los metodos aqui llamados son transacionales
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

                case Events.AccountExternalAccountCreated:
                    await _stripeService.VerifyAccount(item.Account);
                    break;

                case Events.InvoicePaid:
                    var invoice = item.Data.Object as Invoice;
                    await _stripeService.HandleInvoiceWebHook(invoice!);
                    break;
                case Events.InvoiceFinalizationError:
                case Events.InvoiceFinalizationFailed:
                case Events.InvoicePaymentFailed:
                    var invoiceFail = item.Data.Object as Invoice;
                    await _stripeService.HandleInvoiceFailWebHook(invoiceFail!, item.Type);
                    break;

                default:
                    Console.WriteLine("Unhandled event type: {0}", item.Type);
                    break;
            }
            return true;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
}
