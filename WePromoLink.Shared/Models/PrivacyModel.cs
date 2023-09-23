namespace WePromoLink.Models;

public class PrivacyModel
{
    public Guid Id { get; set; }
    public UserModel UserModel { get; set; }
    public Guid UserModelId { get; set; }

    // Profile
    public bool ShowEmailOnProfile { get; set; }
    public bool ShowSocialsOnProfile { get; set; }
    public bool ShowCampaignsOnProfile { get; set; }
    public bool ShowLinksOnProfile { get; set; }
    public bool ShowProfitOnProfile { get; set; }
    public bool ShowAffiliateLinkOnProfile { get; set; }
    public bool ShowQRUrlOnProfile { get; set; }

    // MyPage
    public bool PublicMyPage { get; set; }
    public bool ShowAffiliateLinkOnMyPage { get; set; }
    public bool ShowCallOfActionOnMyPage { get; set; }
    public bool ShowLinksOnMyPage { get; set; }
    public bool ShowSocialsOnMyPage { get; set; }
    public bool UseMyPageTemplate { get; set; }

    public PrivacyModel()
    {
        Id = Guid.NewGuid();
    }
}