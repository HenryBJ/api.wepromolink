namespace WePromoLink.Models;

public class SharedTodayOnCampaignModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public virtual CampaignModel Campaign { get; set; }
    public Guid CampaignModelId { get; set; }
    public int Value { get; set; }
}