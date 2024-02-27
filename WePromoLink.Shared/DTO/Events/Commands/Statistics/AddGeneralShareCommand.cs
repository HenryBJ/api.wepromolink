namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralShareCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddGeneralShareCommand()
    {
        EventType = GetType().FullName!;
    }
}