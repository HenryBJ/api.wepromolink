namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralClickLinkCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public AddGeneralClickLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}