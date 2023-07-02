namespace WePromoLink.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public string? CustomerId { get; set; }
    public string? FirebaseId { get; set; }
    public string? Fullname { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string Email { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsSubscribed { get; set; }
    public string? BlockageCause { get; set; }
    public List<CampaignModel> Campaigns { get; set; }
    public List<LinkModel> Links { get; set; }
    public List<PaymentTransaction> Transactions { get; set; }
    public List<NotificationModel> Notifications { get; set; }
    public virtual SubscriptionModel Subscription { get; set; }
    public Guid SubscriptionModelId { get; set; }
    public DateTime CreatedAt { get; set; }
    public AvailableModel Available { get; set; }
    public BudgetModel Budget { get; set; }
    public LockedModel Locked { get; set; }
    public PayoutStatModel PayoutStat { get; set; }
    public ProfitModel Profit { get; set; }
    public SharedTodayUserModel SharedToday {get; set;}
    public SharedLastWeekUserModel SharedLastWeek { get; set; }
    public ClickLastWeekOnLinksUserModel ClickLastWeekOnLinksUser { get; set; }
    public ClicksLastWeekOnCampaignUserModel ClicksLastWeekOnCampaignUser { get; set; }
    public ClicksTodayOnCampaignUserModel ClicksTodayOnCampaignUser { get; set; }
    public ClicksTodayOnLinksUserModel ClicksTodayOnLinksUser { get; set; }
    public EarnLastWeekUserModel EarnLastWeekUser { get; set; }
    public EarnTodayUserModel EarnTodayUser { get; set; }
    public HistoryClicksByCountriesOnCampaignUserModel HistoryClicksByCountriesOnCampaignUser { get; set; }
    public HistoryClicksByCountriesOnLinkUserModel HistoryClicksByCountriesOnLinkUser { get; set; }
    public HistoryClicksOnCampaignUserModel HistoryClicksOnCampaignUser { get; set; }
    public HistoryClicksOnSharesUserModel HistoryClicksOnSharesUser { get; set; }
    public HistoryEarnByCountriesUserModel HistoryEarnByCountriesUser { get; set; }
    public HistoryEarnOnLinksUserModel HistoryEarnOnLinksUser { get; set; }
    public HistorySharedByUsersUserModel HistorySharedByUsersUser { get; set; }
    public HistoryClicksOnLinksUserModel HistoryClicksOnLinksUser { get; set; }
    public BitcoinBillingMethod BitcoinBillingMethod { get; set; }
    public StripeBillingMethod StripeBillingMethod { get; set; }

}