using MediatR;

namespace WePromoLink.DTO.Events;

public class CampaignSharedEvent : BaseEvent, IRequest<bool>
{
    public Guid OwnerUserId { get; set; }
    public Guid SharedByUserId { get; set; }
    public Guid CampaignId { get; set; }
    public string? OwnerName { get; set; }
    public string? SharedByName { get; set; }
    public string? CampaignName { get; set; }
    public decimal Amount { get; set; }
    public decimal EPM { get; set; }

    public CampaignSharedEvent()
    {
        EventType = GetType().FullName!;
    }

}