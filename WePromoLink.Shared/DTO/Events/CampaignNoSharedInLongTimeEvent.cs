using MediatR;

namespace WePromoLink.DTO.Events;

public class CampaignNoSharedInLongTimeEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public string? Name { get; set; }
    public string? CampaignName { get; set; }
    public DateTime? LastShared { get; set; }
    public int DaysNoShared { get; set; }

    public CampaignNoSharedInLongTimeEvent()
    {
        EventType = GetType().FullName!;
    }

}