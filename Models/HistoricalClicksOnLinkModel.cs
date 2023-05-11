namespace WePromoLink.Models;

public class HistoryClicksOnLinkUserModel:HistoryStatsBaseModel<int>
{
    public Guid Id { get; set; }
    public virtual LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
}