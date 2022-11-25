namespace WePromoLink;

public class SponsoredLinkStats
{
    public int LinkId { get; set; }
    public int Shared { get; set; }
    public int TotalClicks { get; set; }
    public int ValidClicks { get; set; }
    public decimal Spend { get; set; }
    public decimal RemainBudget { get; set; }
    public DateTime LastClick { get; set; }
    public DateTime LastShared { get; set; }
    public DateTime LastUpdated { get; set; }
}