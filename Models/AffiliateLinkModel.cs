namespace WePromoLink.Models;


public class AffiliateLinkModel
{
    public int Id { get; set; }
    public EmailModel Email { get; set; }
    public int? EmailModelId { get; set; }
    public SponsoredLinkModel SponsoredLink { get; set; }
    public int SponsoredLinkModelId { get; set; }
    public string ExternalId { get; set; }
    public string BTCAddress { get; set; }
    public decimal Threshold { get; set; }
    public string? Group { get; set; }
    public decimal TotalEarned { get; set; }
    public decimal TotalWithdraw { get; set; }
    public decimal Available { get; set; }
    public DateTime? LastClick { get; set; }
    public DateTime? LastUpdated { get; set; }
    public DateTime CreatedAt { get; set; }
}