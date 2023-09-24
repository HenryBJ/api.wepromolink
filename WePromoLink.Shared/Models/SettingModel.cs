namespace WePromoLink.Models;

public class SettingModel
{
    public Guid Id { get; set; }
    public virtual UserModel UserModel { get; set; }
    public Guid UserModelId { get; set; }
    public string? Language { get; set; }
    public string? CampaignLanguages { get; set; }

    // Notifications
    public bool CampaignClickedOnNotification { get; set; }
    public bool CampaignClickedOnRealTime { get; set; }
    public bool CampaignClickedOnEmail { get; set; }

    public bool CampaignCreatedOnNotification { get; set; }
    public bool CampaignCreatedOnRealTime { get; set; }
    public bool CampaignCreatedOnEmail { get; set; }

    public bool CampaignDeletedOnNotification { get; set; }
    public bool CampaignDeletedOnRealTime { get; set; }
    public bool CampaignDeletedOnEmail { get; set; }

    public bool CampaignEditedOnNotification { get; set; }
    public bool CampaignEditedOnRealTime { get; set; }
    public bool CampaignEditedOnEmail { get; set; }

    public bool CampaignPublishedOnNotification { get; set; }
    public bool CampaignPublishedOnRealTime { get; set; }
    public bool CampaignPublishedOnEmail { get; set; }

    public bool CampaignSharedOnNotification { get; set; }
    public bool CampaignSharedOnRealTime { get; set; }
    public bool CampaignSharedOnEmail { get; set; }

    public bool CampaignSoldOutOnNotification { get; set; }
    public bool CampaignSoldOutOnRealTime { get; set; }
    public bool CampaignSoldOutOnEmail { get; set; }

    public bool CampaignUnPublishedOnNotification { get; set; }
    public bool CampaignUnPublishedOnRealTime { get; set; }
    public bool CampaignUnPublishedOnEmail { get; set; }

    public bool LinkClickedOnNotification { get; set; }
    public bool LinkClickedOnRealTime { get; set; }
    public bool LinkClickedOnEmail { get; set; }

    public bool LinkCreatedOnNotification { get; set; }
    public bool LinkCreatedOnRealTime { get; set; }
    public bool LinkCreatedOnEmail { get; set; }

    public bool HitGeoLocalizedSuccessOnNotification { get; set; }
    public bool HitGeoLocalizedSuccessOnRealTime { get; set; }
    public bool HitGeoLocalizedSuccessOnEmail { get; set; }

    public SettingModel()
    {
        Id = Guid.NewGuid();
        CampaignClickedOnRealTime = true;
        CampaignPublishedOnNotification = true;
        CampaignPublishedOnRealTime = true;
        CampaignDeletedOnNotification = true;
        CampaignDeletedOnRealTime = true;
        CampaignSharedOnRealTime = true;
        CampaignSoldOutOnNotification = true;
        CampaignSoldOutOnRealTime = true;
        CampaignSoldOutOnEmail = true;
        LinkClickedOnRealTime = true;
        HitGeoLocalizedSuccessOnRealTime = true;

    }

}