namespace WePromoLink.DTO.Events;

public class BaseEvent 
{
    public string EventType { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public BaseEvent()
    {
        CreatedAt = DateTime.UtcNow;
    }
}