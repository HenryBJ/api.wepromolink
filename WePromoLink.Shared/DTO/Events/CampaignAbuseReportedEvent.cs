using MediatR;

namespace WePromoLink.DTO.Events;

public class CampaignAbuseReportedEvent : BaseEvent, IRequest<bool>
{
    public Guid OwnerUserId { get; set; }
    public Guid ReporterUserId { get; set; }
    public Guid CampaignId { get; set; }
    public string? OwnerName { get; set; }
    public string? ReporterName { get; set; }
    public string? CampaignName { get; set; }
    public string? CampaignDescription { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Amount { get; set; }
    public decimal EPM { get; set; }

    public CampaignAbuseReportedEvent()
    {
        EventType = GetType().FullName!;
    }

}