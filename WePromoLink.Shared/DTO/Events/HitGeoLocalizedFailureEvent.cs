using MediatR;

namespace WePromoLink.DTO.Events;

public class HitGeoLocalizedFailureEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public Guid LinkId { get; set; }
    public string? OwnerName { get; set; }
    public string? CampaignName { get; set; }
    public string? FailureReason { get; set; }
    public int Attempt { get; set; }

    public HitGeoLocalizedFailureEvent()
    {
        EventType = GetType().FullName!;
    }

}