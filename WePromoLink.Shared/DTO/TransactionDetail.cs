using WePromoLink.DTO;

namespace WePromoLink;

public class TransactionDetail
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public string Status { get; set; }
    public string? CampaignName { get; set; }
    public ImageData? ImageBundle { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}