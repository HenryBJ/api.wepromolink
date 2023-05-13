using WePromoLink.Interfaces;

namespace WePromoLink.Models;

public class SharedTodayUserModel: StatsBaseModel, IHasValue<int>
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public int Value { get; set; }
}