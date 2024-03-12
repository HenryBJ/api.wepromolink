namespace WePromoLink.Models;

public class SubscriptionFeatureModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? BoolValue { get; set; }
    public string? Value { get; set; }
    public int Order { get; set; }
    public Guid SubscriptionPlanModelId { get; set; }

    public SubscriptionFeatureModel()
    {
        Id = Guid.NewGuid();
    }
}