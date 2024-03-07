namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralGeoPinCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public AddGeneralGeoPinCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}