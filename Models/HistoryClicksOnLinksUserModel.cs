namespace WePromoLink.Models;

public class HistoryClicksOnLinksUserModel:HistoryStatsBaseModel<int>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
}