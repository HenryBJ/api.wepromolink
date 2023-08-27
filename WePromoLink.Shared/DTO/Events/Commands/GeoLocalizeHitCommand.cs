namespace WePromoLink.DTO.Events.Commands;

public class GeoLocalizeHitCommand: BaseEvent
{
    public Guid HitId { get; set; }
    public GeoLocalizeHitCommand()
    {
        EventType = GetType().FullName!;
    }
}