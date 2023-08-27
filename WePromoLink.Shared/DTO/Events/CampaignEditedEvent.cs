using MediatR;

namespace WePromoLink.DTO.Events;

public class CampaignEditedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public string? Name { get; set; }
    public string? CampaignNameOld { get; set; }
    public string? CampaignNameNew { get; set; }
    public decimal AmountOld { get; set; }
    public decimal AmountNew { get; set; }
    public decimal EPMOld { get; set; }
    public decimal EPMNew { get; set; }

    public CampaignEditedEvent()
    {
        EventType = GetType().FullName!;
    }

}