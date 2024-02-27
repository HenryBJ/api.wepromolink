namespace WePromoLink.DTO.Events.Commands.Statistics;

public class ReduceProfitCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public decimal Amount { get; set; }
    public ReduceProfitCommand()
    {
        EventType = GetType().FullName!;
    }
}