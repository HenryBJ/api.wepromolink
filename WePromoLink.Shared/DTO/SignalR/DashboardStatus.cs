namespace WePromoLink.DTO.SignalR;

public class DashboardStatus
{
    public int RegisteredUsers { get; set; }
    public int VerifiedUsers { get; set; }
    public int UnVerifiedUsers { get; set; }
    public int Campaigns { get; set; }
    public int Shareds { get; set; }
    public int Hits { get; set; }
    public int Clicks { get; set; }
    public int Transactions { get; set; }
    public int Deposit { get; set; }
    public int Withdraw { get; set; }
    public decimal CampaignBudget { get; set; }
    public decimal TotalAvailable { get; set; }
    public decimal TotalProfit { get; set; }
    public decimal TotalFee { get; set; }
    public DateTime LastModified { get; set; }

    public DashboardStatus()
    {
        LastModified = DateTime.UtcNow;
    }
}