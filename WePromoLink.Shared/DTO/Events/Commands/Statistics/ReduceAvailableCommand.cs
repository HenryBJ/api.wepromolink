namespace WePromoLink.DTO.Events.Commands.Statistics;

public class ReduceAvailableCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Amount { get; set; }
    public ReduceAvailableCommand()
    {
        EventType = GetType().FullName!;
    }
}