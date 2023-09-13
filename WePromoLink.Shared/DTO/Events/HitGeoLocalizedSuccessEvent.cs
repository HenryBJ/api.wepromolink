using MediatR;

namespace WePromoLink.DTO.Events;

public class HitGeoLocalizedSuccessEvent : BaseEvent, IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CampaignId { get; set; }
    public Guid LinkId { get; set; }
    public string? OwnerName { get; set; }
    public string? CampaignName { get; set; }
    public string? Country { get; set; }
    public string? FlagUrl { get; set; }
    public decimal? Longitud { get; set; }
    public decimal? Latitud { get; set; }
    public bool FirstTime { get; set; }

    public HitGeoLocalizedSuccessEvent()
    {
        EventType = GetType().FullName!;
    }

}