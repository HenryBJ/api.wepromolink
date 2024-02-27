namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickCountryLinkCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public string Country { get; set; }
    public AddClickCountryLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}