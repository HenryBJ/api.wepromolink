namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public AddClickCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}