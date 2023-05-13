using WePromoLink.Interfaces;

namespace WePromoLink.Models;

public class ProfitModel: StatsBaseModel, IHasValue<decimal>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public decimal Value { get; set; }
}