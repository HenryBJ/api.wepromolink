using MediatR;

namespace WePromoLink.DTO.Events;

public class UserSubscriptionExpiredEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid Subscription { get; set; }
    public string? Name { get; set; }
    public string? SubscriptionPlanName { get; set; }

    public UserSubscriptionExpiredEvent()
    {
        EventType = GetType().FullName!;
    }

}