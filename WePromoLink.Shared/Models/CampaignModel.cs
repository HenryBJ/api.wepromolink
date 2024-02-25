namespace WePromoLink.Models;

public class CampaignModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public List<LinkModel> Links { get; set; }
    public List<PaymentTransaction> Transactions { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
    public string Url { get; set; }
    public ImageDataModel? ImageData { get; set; }
    public Guid? ImageDataModelId { get; set; }
    public decimal Budget { get; set; }
    public decimal EPM { get; set; }
    public bool IsArchived { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime? LastClick { get; set; }
    public DateTime? LastShared { get; set; }
    public DateTime? LastUpdated { get; set; }
    public DateTime CreatedAt { get; set; }

}