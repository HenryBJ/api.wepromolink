namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddSpendCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Spend { get; set; }
    public AddSpendCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}