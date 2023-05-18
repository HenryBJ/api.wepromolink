namespace WePromoLink.Models;

public class HistoryClicksByCountriesOnCampaignUserModel:HistoryStatsBaseModel<int, string>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
}