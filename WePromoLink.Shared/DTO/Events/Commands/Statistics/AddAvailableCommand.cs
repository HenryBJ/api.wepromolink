namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddAvailableCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public decimal Available { get; set; }
    public AddAvailableCommand()
    {
        EventType = GetType().FullName!;
    }
}