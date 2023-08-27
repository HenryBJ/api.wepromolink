using MediatR;

namespace WePromoLink.DTO.Events;

public class ProfitReachWihtdrawThresholdEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public decimal Profit { get; set; }
    public decimal WihtdrawThreshold { get; set; }

    public ProfitReachWihtdrawThresholdEvent()
    {
        EventType = GetType().FullName!;
    }

}