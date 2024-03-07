namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddShareCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public AddShareCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}