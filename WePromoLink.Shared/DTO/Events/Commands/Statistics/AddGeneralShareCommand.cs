namespace WePromoLink.DTO.Events.Commands.Statistics;

public class AddGeneralShareCommand: StatsBaseCommand
{
    public string ExternalId { get; set; }
    public AddGeneralShareCommand()
    {
        EventType = GetType().FullName!;
    }
}