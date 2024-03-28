namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeoPinCampaignCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public AddGeoPinCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}