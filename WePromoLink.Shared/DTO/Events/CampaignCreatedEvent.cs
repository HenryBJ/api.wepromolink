using MediatR;

namespace WePromoLink.DTO.Events;

public class CampaignCreatedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public string? Name { get; set; }
    public string? CampaignName { get; set; }
    public decimal InitialAmount { get; set; }
    public decimal EPM { get; set; }

    public CampaignCreatedEvent()
    {
        EventType = GetType().FullName!;
    }

}