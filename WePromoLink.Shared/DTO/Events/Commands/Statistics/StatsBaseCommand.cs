namespace WePromoLink.DTO.Events.Commands.Statistics;

public class StatsBaseCommand 
{
    public string EventType { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public StatsBaseCommand()
    {
        CreatedAt = DateTime.UtcNow;
    }
}