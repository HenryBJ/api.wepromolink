using MediatR;

namespace WePromoLink.DTO.Events;

public class UserLoggedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public UserLoggedEvent()
    {
        EventType = GetType().FullName!;
    }

}