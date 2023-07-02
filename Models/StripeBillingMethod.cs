namespace WePromoLink.Models;

public class StripeBillingMethod : BillingMethodBase
{
    public string? AccountId { get; set; }

    public StripeBillingMethod()
    {
        Id = Guid.NewGuid();

    }
}