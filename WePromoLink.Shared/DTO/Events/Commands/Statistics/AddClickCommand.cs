namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddClickCommand()
    {
        EventType = GetType().FullName!;
    }
}