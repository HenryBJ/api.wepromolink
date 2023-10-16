namespace WePromoLink.DTO.SubscriptionPlan;

public class SubscriptionPlanFeatureCreate
{
    public string Name { get; set; }
    public bool? BoolValue { get; set; }
    public string? Value { get; set; }
    public Guid? SubscrioptionPlanId { get; set; }
}