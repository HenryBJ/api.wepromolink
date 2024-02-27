namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickCountryCampaignCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public string Country { get; set; }
    public AddClickCountryCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}