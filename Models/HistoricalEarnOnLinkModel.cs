namespace WePromoLink.Models;

public class HistoryEarnOnLinkModel:HistoryStatsBaseModel<decimal, DateTime>
{
    public Guid Id { get; set; }
    public virtual LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
}