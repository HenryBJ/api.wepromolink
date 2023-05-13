using WePromoLink.Interfaces;

namespace WePromoLink.Models;

public class AvailableModel: StatsBaseModel, IHasValue<decimal>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public decimal Value { get; set; }
}