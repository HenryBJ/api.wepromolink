using MediatR;

namespace WePromoLink.DTO.Events;

public class WithdrawCompletedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid PaymentTransactionId { get; set; }
    public string? Name { get; set; }
    public decimal? Fee { get; set; }
    public string Email { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal Amount { get; set; }

    public WithdrawCompletedEvent()
    {
        EventType = GetType().FullName!;
    }

}