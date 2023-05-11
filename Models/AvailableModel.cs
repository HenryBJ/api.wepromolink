namespace WePromoLink.Models;

public class AvailableModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public decimal Value { get; set; }
}