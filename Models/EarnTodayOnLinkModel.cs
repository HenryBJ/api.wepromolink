using WePromoLink.Interfaces;

namespace WePromoLink.Models;

public class EarnTodayOnLinkModel: StatsBaseModel, IHasValue<decimal>
{
    public Guid Id { get; set; }
    public virtual LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
    public decimal Value { get; set; }
}