namespace WePromoLink.Models;

public class ClicksTodayOnCampaignUserModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public int Value { get; set; }
}