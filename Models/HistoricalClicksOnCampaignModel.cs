namespace WePromoLink.Models;

public class HistoryClicksOnCampaignModel:HistoryStatsBaseModel<int>
{
    public Guid Id { get; set; }
    public virtual CampaignModel Campaign { get; set; }
    public Guid CampaignModelId { get; set; }
}