namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralClickCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public AddGeneralClickCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}