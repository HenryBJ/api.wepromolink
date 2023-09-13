using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.NotiWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 5;
    private readonly ILogger<Worker> _logger;
    private MessageBroker<BaseEvent> _eventMessageBroker;
    private readonly ISender _sender;

    public Worker(ILogger<Worker> logger, MessageBroker<BaseEvent> eventMessageBroker, ISender sender)
    {
        _logger = logger;
        _eventMessageBroker = eventMessageBroker;
        _sender = sender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _eventMessageBroker.ReceiveBaseEvent((item) => ProcessEvent(item).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> ProcessEvent(BaseEvent ev)
    {
        try
        {
            _logger.LogInformation($"Recibido {ev.EventType}");
            switch (ev)
            {
                case UserRegisteredEvent userRegisteredEvent: return await _sender.Send(userRegisteredEvent);
                case UserLoggedEvent userLoggedEvent: return await _sender.Send(userLoggedEvent);
                case UserBlockedEvent userBlockedEvent: return await _sender.Send(userBlockedEvent);
                case UserSubscriptionExpiredEvent userSubscriptionExpiredEvent: return await _sender.Send(userSubscriptionExpiredEvent);
                case CampaignAbuseReportedEvent campaignAbuseReportedEvent: return await _sender.Send(campaignAbuseReportedEvent);
                case CampaignClickedEvent campaignClickedEvent: return await _sender.Send(campaignClickedEvent);
                case CampaignCreatedEvent campaignCreatedEvent: return await _sender.Send(campaignCreatedEvent);
                case CampaignDeletedEvent campaignDeletedEvent: return await _sender.Send(campaignDeletedEvent);
                case CampaignEditedEvent campaignEditedEvent: return await _sender.Send(campaignEditedEvent);
                case CampaignNoSharedInLongTimeEvent campaignNoSharedInLongTimeEvent: return await _sender.Send(campaignNoSharedInLongTimeEvent);
                case CampaignPublishedEvent campaignPublishedEvent: return await _sender.Send(campaignPublishedEvent);
                case CampaignSharedEvent campaignSharedEvent: return await _sender.Send(campaignSharedEvent);
                case CampaignSoldOutEvent campaignSoldOutEvent: return await _sender.Send(campaignSoldOutEvent);
                case CampaignUnPublishedEvent campaignUnPublishedEvent: return await _sender.Send(campaignUnPublishedEvent);
                case LinkClickedEvent linkClickedEvent: return await _sender.Send(linkClickedEvent);
                case LinkCreatedEvent linkCreatedEvent: return await _sender.Send(linkCreatedEvent);
                case LinkNoClickedInLongTimeEvent linkNoClickedInLongTimeEvent: return await _sender.Send(linkNoClickedInLongTimeEvent);
                case HitClickedEvent hitClickedEvent: return await _sender.Send(hitClickedEvent);
                case HitCreatedEvent hitCreatedEvent: return await _sender.Send(hitCreatedEvent);
                case HitGeoLocalizedFailureEvent hitGeoLocalizedFailureEvent: return await _sender.Send(hitGeoLocalizedFailureEvent);
                case HitGeoLocalizedSuccessEvent hitGeoLocalizedSuccessEvent: return await _sender.Send(hitGeoLocalizedSuccessEvent);
                case DepositCompletedEvent depositCompletedEvent: return await _sender.Send(depositCompletedEvent);
                case DepositFailureEvent depositFailureEvent: return await _sender.Send(depositFailureEvent);
                case WithdrawCompletedEvent withdrawCompletedEvent: return await _sender.Send(withdrawCompletedEvent);
                case WithdrawFailureEvent withdrawFailureEvent: return await _sender.Send(withdrawFailureEvent);
                case ProfitReachWihtdrawThresholdEvent profitReachWihtdrawThresholdEvent: return await _sender.Send(profitReachWihtdrawThresholdEvent);
                default: return false;
            }

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }

    }
}
