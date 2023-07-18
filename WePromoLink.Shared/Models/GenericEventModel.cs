namespace WePromoLink.Models;

public class GenericEventModel
{
    public Guid Id { get; set; }
    public string Source { get; set; }
    public string Message { get; set; }
    public string EventType { get; set; }
    public DateTime CreatedAt { get; set; }
}