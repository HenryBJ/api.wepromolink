namespace WePromoLink.DTO;

public class PricingCard
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string? MonthlyPriceId { get; set; }
    public string? AnnualyPriceId { get; set; }
    public decimal Monthly { get; set; }
    public decimal Annually { get; set; }
    public decimal Discount { get; set; }
    public decimal Commission { get; set; }
    public string PaymentMethod { get; set; }
    public string Tag { get; set; }
    public int Order { get; set; }
    public List<PricingFeature> Features { get; set; }


}