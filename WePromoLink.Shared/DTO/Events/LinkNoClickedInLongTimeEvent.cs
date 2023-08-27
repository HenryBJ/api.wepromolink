using MediatR;

namespace WePromoLink.DTO.Events;

public class LinkNoClickedInLongTimeEvent : BaseEvent, IRequest<bool>
{
    public Guid OwnerUserId { get; set; }
    public Guid LinkCreatorUserId { get; set; }
    public Guid CampaignId { get; set; }
    public Guid LinkId { get; set; }
    public string? OwnerName { get; set; }
    public string? LinkCreatorName { get; set; }
    public string? CampaignName { get; set; }
    public DateTime? LastClicked { get; set; }
    public int DaysNoClicked { get; set; }

    public LinkNoClickedInLongTimeEvent()
    {
        EventType = GetType().FullName!;
    }

}