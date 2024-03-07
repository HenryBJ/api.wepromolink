namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralGeoPinLinkCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public AddGeneralGeoPinLinkCommand()
    {
        EventType = GetType().FullName!;
    }
}