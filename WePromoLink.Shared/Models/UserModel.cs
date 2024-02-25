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
    public decimal Budget { get; set; }
    public decimal Profit { get; set; }
    public decimal Available { get; set; }
    public string? BlockageCause { get; set; }
    public List<CampaignModel> Campaigns { get; set; }
    public List<LinkModel> Links { get; set; }
    public List<PaymentTransaction> Transactions { get; set; }
    public List<NotificationModel> Notifications { get; set; }
    public virtual SubscriptionModel Subscription { get; set; }
    public Guid SubscriptionModelId { get; set; }
    public DateTime CreatedAt { get; set; }
    public BitcoinBillingMethod BitcoinBillingMethod { get; set; }
    public StripeBillingMethod StripeBillingMethod { get; set; }
    public ProfileModel Profile { get; set; }
    public MyPageModel MyPage { get; set; }
    public SettingModel Setting { get; set; }
    public PrivacyModel Privacy { get; set; }
    public AffiliateModel AffiliateProgram { get; set; }
    public List<AffiliatedUserModel> AffiliatedUsers { get; set; }

}