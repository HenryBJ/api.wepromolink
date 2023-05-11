namespace WePromoLink.Models;

public class HistoryEarnOnLinkModel:HistoryStatsBaseModel<decimal>
{
    public Guid Id { get; set; }
    public virtual LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
}