namespace WePromoLink.Models;

public class ClickLastWeekOnLinksUserModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public int Value { get; set; }
}