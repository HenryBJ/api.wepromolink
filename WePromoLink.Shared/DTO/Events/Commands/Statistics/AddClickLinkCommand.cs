namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickLinkCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public AddClickLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}