namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddBudgetCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Budget { get; set; }
    public AddBudgetCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}