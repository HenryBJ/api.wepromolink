namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickLinkCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddClickLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}