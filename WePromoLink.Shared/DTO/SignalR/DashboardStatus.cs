namespace WePromoLink.DTO.SignalR;

public class DashboardStatus
{
    public long RegisteredUsers { get; set; }
    public long VerifiedUsers { get; set; }
    public long UnVerifiedUsers { get; set; }
    public long Campaigns { get; set; }
    public long Shareds { get; set; }
    public long Hits { get; set; }
    public long Clicks { get; set; }
    public long Transactions { get; set; }
    public long Deposit { get; set; }
    public long Withdraw { get; set; }
    public long CampaignReported { get; set; }
    public long GeoLocations { get; set; }
    public decimal CampaignBudget { get; set; }
    public decimal TotalAvailable { get; set; }
    public decimal TotalProfit { get; set; }
    public DateTime LastModified { get; set; }

    public DashboardStatus()
    {
        LastModified = DateTime.UtcNow;
    }
}