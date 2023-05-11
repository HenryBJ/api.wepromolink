namespace WePromoLink.Models;

public class SubscriptionPlanModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public string? MonthlyProductId { get; set; }
    public string? AnnualyProductId { get; set; }
    public string MonthlyPaymantLink { get; set; }
    public string AnnualyPaymantLink { get; set; }
    public List<SubscriptionModel> Subscriptions { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public decimal Monthly { get; set; }
    public decimal Annually { get; set; }
    public decimal Discount { get; set; }
    public bool ContainAds { get; set; }
    public decimal DepositFee { get; set; }
    public decimal PayoutFee { get; set; }
    public decimal PayoutMinimun { get; set; }
    public string PaymentMethod { get; set; }
    public string Tag { get; set; }
    public string? Metadata { get; set; }

}