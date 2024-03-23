namespace WePromoLink.Models;

public class AffiliatedUserModel 
{
    public Guid Id { get; set; }
    public Guid UserModelId { get; set; }
    public UserModel User { get; set; }
    public Guid ParentId { get; set; }
    public UserModel Parent { get; set; }
    public bool Active { get; set; }
    public decimal TotalPayments { get; set; }
    public int NumberPayments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }

    public AffiliatedUserModel()
    {
        Id = Guid.NewGuid();
    }
}