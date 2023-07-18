namespace WePromoLink.Models;

public class HistoryEarnByCountriesUserModel:HistoryStatsBaseModel<decimal, string>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
}