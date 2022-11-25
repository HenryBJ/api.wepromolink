namespace WePromoLink;

public class AffiliateLinkStats
{
    public string AffLinkId { get; set; }
    public string SponsoredLinkId { get; set; }
    public int TotalClicks { get; set; }
    public int ValidClicks { get; set; }
    public decimal TotalEarned { get; set; }
    public decimal TotalWithdraw { get; set; }
    public decimal Available { get; set; }
    public DateTime LastClick { get; set; }
    public DateTime LastUpdated { get; set; }
}