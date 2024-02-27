namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddSpendCampaignCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public decimal Spend { get; set; }
    public AddSpendCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}