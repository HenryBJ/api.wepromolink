namespace WePromoLink.Models;

public class PaymentTransaction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? AffiliateLinkId { get; set; }
    public int? SponsoredLinkId { get; set; }
    public decimal Amount { get; set; }
    public bool IsDeposit { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}