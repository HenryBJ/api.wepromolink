namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddShareCampaignCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddShareCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}