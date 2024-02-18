namespace WePromoLink.Models;

public class SubscriptionPlanModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public int Level { get; set; }
    public string? MonthlyPriceId { get; set; }
    public string? AnnualyPriceId { get; set; }
    public List<SubscriptionModel> Subscriptions { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public decimal Monthly { get; set; }
    public decimal Commission { get; set; }
    public decimal Annually { get; set; }
    public decimal Discount { get; set; }
    public string PaymentMethod { get; set; }
    public string Tag { get; set; }
    public List<SubscriptionFeatureModel> Features { get; set; }
    public string? Metadata { get; set; }

}