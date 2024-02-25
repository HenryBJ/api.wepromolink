using MediatR;

namespace WePromoLink.DTO.Events;

public class WithdrawFailureEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? PaymentMethod { get; set; }
    public string? FailureReason { get; set; }
    public decimal Amount { get; set; }

    public WithdrawFailureEvent()
    {
        EventType = GetType().FullName!;
    }

}