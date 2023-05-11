namespace WePromoLink.Models;

public class HistoryClicksOnLinkModel:HistoryStatsBaseModel<int>
{
    public Guid Id { get; set; }
    public Guid LinkModelId { get; set; }
}