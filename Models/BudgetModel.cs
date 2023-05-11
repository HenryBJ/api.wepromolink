namespace WePromoLink.Models;

public class BudgetModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public decimal Value { get; set; }
}