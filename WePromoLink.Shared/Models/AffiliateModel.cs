namespace WePromoLink.Models;

public class AffiliateModel
{
    public Guid Id { get; set; }
    public virtual UserModel UserModel { get; set; }
    public Guid UserModelId { get; set; }
    public bool Active { get; set; }
    public string AffiliateLink { get; set; }
    public int Affiliates { get; set; }
    public decimal Profit { get; set; }
    public decimal? MRR { get; set; } // monthly recurrent revenue
    public decimal? OTR { get; set; } // one time revenue
    public DateTime? LastModified { get; set; }

    public AffiliateModel()
    {
        Id = Guid.NewGuid();
    }
    
}