using MediatR;

namespace WePromoLink.DTO.Events;

public class DepositFailureEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid PaymentTransactionId { get; set; }
    public string? Name { get; set; }
    public string? PaymentMethod { get; set; }
    public string? FailureReason { get; set; }
    public decimal Amount { get; set; }

    public DepositFailureEvent()
    {
        EventType = GetType().FullName!;
    }

}