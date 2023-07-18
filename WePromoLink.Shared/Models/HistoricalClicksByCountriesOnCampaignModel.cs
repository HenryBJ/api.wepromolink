namespace WePromoLink.Models;

public class HistoryClicksByCountriesOnCampaignModel:HistoryStatsBaseModel<int, string>
{
    public Guid Id { get; set; }
    public virtual CampaignModel Campaign { get; set; }
    public Guid CampaignModelId { get; set; }
}