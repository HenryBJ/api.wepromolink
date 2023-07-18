namespace WePromoLink.Models;

public class HistorySharedOnCampaignModel : HistoryStatsBaseModel<int, DateTime>
{
    public Guid Id { get; set; }
    public virtual CampaignModel Campaign { get; set; }
    public Guid CampaignModelId { get; set; }

    public HistorySharedOnCampaignModel()
    {
        X0 = 0;
        X1 = 0;
        X2 = 0;
        X3 = 0;
        X4 = 0;
        X5 = 0;
        X6 = 0;
        X7 = 0;
        X8 = 0;
        X9 = 0;
        L0 = DateTime.UtcNow.AddDays(-9);
        L1 = DateTime.UtcNow.AddDays(-8);
        L2 = DateTime.UtcNow.AddDays(-7);
        L3 = DateTime.UtcNow.AddDays(-6);
        L4 = DateTime.UtcNow.AddDays(-5);
        L5 = DateTime.UtcNow.AddDays(-4);
        L6 = DateTime.UtcNow.AddDays(-3);
        L7 = DateTime.UtcNow.AddDays(-2);
        L8 = DateTime.UtcNow.AddDays(-1);
        L9 = DateTime.UtcNow;
    }
}