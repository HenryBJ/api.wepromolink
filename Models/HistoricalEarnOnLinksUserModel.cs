namespace WePromoLink.Models;

public class HistoryEarnOnLinksUserModel:HistoryStatsBaseModel<decimal>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
}