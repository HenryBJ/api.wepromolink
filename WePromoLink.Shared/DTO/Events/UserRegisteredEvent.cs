using MediatR;

namespace WePromoLink.DTO.Events;

public class UserRegisteredEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public UserRegisteredEvent()
    {
        EventType = GetType().FullName!;
    }

}