namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddProfitLinkCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public decimal Profit { get; set; }
    public AddProfitLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}