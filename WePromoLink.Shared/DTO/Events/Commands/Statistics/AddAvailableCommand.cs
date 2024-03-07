namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddAvailableCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Available { get; set; }
    public AddAvailableCommand()
    {
        EventType = GetType().FullName!;
    }
}