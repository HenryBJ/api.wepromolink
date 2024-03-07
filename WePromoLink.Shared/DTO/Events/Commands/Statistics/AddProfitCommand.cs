namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddProfitCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Profit { get; set; }
    public AddProfitCommand()
    {
        EventType = GetType().FullName!;
    }
}