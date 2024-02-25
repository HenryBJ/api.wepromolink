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

}