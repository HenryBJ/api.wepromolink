namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralClickLinkCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddGeneralClickLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}