using MediatR;

namespace WePromoLink.DTO.Events;

public class LinkClickedEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public Guid LinkId { get; set; }
    public string? OwnerName { get; set; }
    public string? LinkCreatorName { get; set; }
    public string? CampaignName { get; set; }

    public LinkClickedEvent()
    {
        EventType = GetType().FullName!;
    }

}