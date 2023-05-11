namespace WePromoLink.Models;

public class EarnLastWeekOnLinkModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public virtual LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
    public decimal Value { get; set; }
}