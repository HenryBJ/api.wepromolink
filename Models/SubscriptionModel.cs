namespace WePromoLink.Models;

public class SubscriptionModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public string Status { get; set; }
    public virtual UserModel User { get; set; }
    public Guid SubscriptionPlanModelId { get; set; }
    public SubscriptionPlanModel SubscriptionPlan { get; set; }
    public DateTime? NextPayment { get; set; }
    public DateTime? LastPayment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public DateTime? CanceledAt { get; set; }
    public bool IsExpired { get; set; }
    public bool IsCanceled { get; set; }

}