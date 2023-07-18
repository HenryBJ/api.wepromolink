namespace WePromoLink.DTO;

public class SubscriptionInfo
{
    public string Status { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public DateTime? NextPaymentDate { get; set; }
    
}