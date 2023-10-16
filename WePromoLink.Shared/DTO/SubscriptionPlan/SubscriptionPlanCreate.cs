namespace WePromoLink.DTO.SubscriptionPlan;

public class SubscriptionPlanCreate
{
    public decimal Monthly { get; set; }
    public string? MonthlyProductId { get; set; }
    public string? AnnualyProductId { get; set; }
    public string MonthlyPaymantLink { get; set; }
    public string AnnualyPaymantLink { get; set; }
    public int Order { get; set; }
    public int Level { get; set; }
    public string Title { get; set; }
    public decimal Annually { get; set; }
    public decimal Discount { get; set; }
    public string PaymentMethod { get; set; }
    public string Tag { get; set; }
    public List<SubscriptionPlanFeatureCreate> Features { get; set; }
}