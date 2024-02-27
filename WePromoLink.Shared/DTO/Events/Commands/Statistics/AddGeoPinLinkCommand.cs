namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeoPinLinkCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public AddGeoPinLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}