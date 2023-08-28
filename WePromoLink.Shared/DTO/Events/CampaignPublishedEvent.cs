using MediatR;

namespace WePromoLink.DTO.Events;

public class CampaignPublishedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public string? Name { get; set; }
    public string? CampaignName { get; set; }
    public decimal Amount { get; set; }

    public CampaignPublishedEvent()
    {
        EventType = GetType().FullName!;
    }

}