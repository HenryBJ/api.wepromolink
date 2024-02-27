namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddClickCampaignCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddClickCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}