using MediatR;

namespace WePromoLink.DTO.Events;

public class UserBlockedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Reason { get; set; }

    public UserBlockedEvent()
    {
        EventType = GetType().FullName!;
    }

}