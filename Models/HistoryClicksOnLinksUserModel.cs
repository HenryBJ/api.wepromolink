namespace WePromoLink.Models;

public class HistoryClicksOnLinksUserModel:HistoryStatsBaseModel<int>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }

    public HistoryClicksOnLinksUserModel()
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
        L0 = DateTime.UtcNow.DayOfWeek.ToString();
        L1 = DateTime.UtcNow.AddDays(1).DayOfWeek.ToString();
        L2 = DateTime.UtcNow.AddDays(2).DayOfWeek.ToString();
        L3 = DateTime.UtcNow.AddDays(3).DayOfWeek.ToString();
        L4 = DateTime.UtcNow.AddDays(4).DayOfWeek.ToString();
        L5 = DateTime.UtcNow.AddDays(5).DayOfWeek.ToString();
        L6 = DateTime.UtcNow.AddDays(6).DayOfWeek.ToString();
        L7 = DateTime.UtcNow.AddDays(7).DayOfWeek.ToString();
        L8 = DateTime.UtcNow.AddDays(8).DayOfWeek.ToString();
        L9 = DateTime.UtcNow.AddDays(9).DayOfWeek.ToString();        
    }
}