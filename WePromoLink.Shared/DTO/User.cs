namespace WePromoLink.DTO;

public class User
{
    public SubscriptionInfo SubscriptionInfo { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string? CustomerId { get; set; }
    public Guid? SubscriptionPlanModelId { get; set; }
    public string ProductId { get; set; }
    public string PhotoUrl { get; set; }

}