namespace WePromoLink.DTO.Events.Commands.Statistics;

public class ReduceBudgetCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Amount { get; set; }
    public ReduceBudgetCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}