namespace WePromoLink.Models;


public class LinkModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public string Url { get; set; }
    public Guid UserModelId { get; set; }
    public CampaignModel Campaign { get; set; }
    public Guid CampaignModelId { get; set; }
    public List<PaymentTransaction> Transactions { get; set; }
    public List<HitModel> Hits { get; set; }
    public string ExternalId { get; set; }
    public DateTime? LastClick { get; set; }
    public DateTime? LastUpdated { get; set; }
    public DateTime CreatedAt { get; set; }
    public ClicksLastWeekOnLinkModel ClicksLastWeekOnLink { get; set; }
    public ClicksTodayOnLinkModel ClicksTodayOnLink { get; set; }
    public EarnLastWeekOnLinkModel EarnLastWeekOnLink { get; set; }
    public EarnTodayOnLinkModel EarnTodayOnLink { get; set; }
    public HistoryClicksByCountriesOnLinkModel HistoryClicksByCountriesOnLink { get; set; }
    public HistoryEarnByCountriesOnLinkModel HistoryEarnByCountriesOnLink { get; set; }
    public HistoryEarnOnLinkModel HistoryEarnOnLink { get; set; }
    public HistoryClicksOnLinkModel HistoryClicksOnLink { get; set; }

}