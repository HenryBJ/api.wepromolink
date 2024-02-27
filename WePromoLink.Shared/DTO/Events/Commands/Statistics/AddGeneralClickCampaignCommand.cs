namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralClickCampaignCommand: BaseEvent
{
    public string ExternalId { get; set; }
    public AddGeneralClickCampaignCommand()
    {
        EventType = GetType().FullName!;
    }
}