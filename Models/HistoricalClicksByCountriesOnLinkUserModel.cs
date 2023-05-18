namespace WePromoLink.Models;

public class HistoryClicksByCountriesOnLinkUserModel:HistoryStatsBaseModel<int, string>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
}