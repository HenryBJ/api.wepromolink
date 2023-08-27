using MediatR;

namespace WePromoLink.DTO.Events;

public class LinkCreatedEvent : BaseEvent, IRequest<bool>
{
    public Guid OwnerUserId { get; set; }
    public Guid LinkCreatorUserId { get; set; }
    public Guid CampaignId { get; set; }
    public Guid LinkId { get; set; }
    public string? OwnerName { get; set; }
    public string? LinkCreatorName { get; set; }
    public string? CampaignName { get; set; }

    public LinkCreatedEvent()
    {
        EventType = GetType().FullName!;
    }

}