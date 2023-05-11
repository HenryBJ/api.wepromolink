namespace WePromoLink.Models;

public class ClicksTodayOnLinkModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public virtual LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
    public int Value { get; set; }
}