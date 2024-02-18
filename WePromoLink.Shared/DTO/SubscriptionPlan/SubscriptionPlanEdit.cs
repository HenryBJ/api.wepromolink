namespace WePromoLink.DTO.SubscriptionPlan;

public class SubscriptionPlanEdit
{
    public Guid Id { get; set; }
    public decimal Monthly { get; set; }
    public string? MonthlyPriceId { get; set; }
    public string? AnnualyPriceId { get; set; }
    public int Order { get; set; }
    public int Level { get; set; }
    public string Title { get; set; }
    public decimal Annually { get; set; }
    public decimal Commission { get; set; }
    public decimal Discount { get; set; }
    public string PaymentMethod { get; set; }
    public string Tag { get; set; }
}