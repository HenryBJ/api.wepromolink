namespace WePromoLink.Models;

public abstract class BillingMethodBase
{
    public Guid Id { get; set; }
    public Guid UserModelId { get; set; }
    public virtual UserModel User { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }

    public BillingMethodBase()
    {
        CreatedAt = DateTime.UtcNow;
    }
}