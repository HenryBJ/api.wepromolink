namespace WePromoLink.DTO;

public class PricingCard
{
    public string Id { get; set; }
    public string Title { get; set; }
    public decimal Monthly { get; set; }
    public decimal Annually { get; set; }
    public decimal Discount { get; set; }
    public decimal DepositFee { get; set; }
    public decimal PayoutFee { get; set; }
    public decimal PayoutMinimun { get; set; }
    public string PaymentMethod { get; set; }
    public string Tag { get; set; }
    public string MonthlyPaymantLink { get; set; }
    public string AnnualyPaymantLink { get; set; }
    public int Order { get; set; }
    public List<PricingFeature> Features { get; set; }


}