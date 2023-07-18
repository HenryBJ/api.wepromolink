namespace WePromoLink.Models;

public class BitcoinBillingMethod: BillingMethodBase
{
    public string? Address { get; set; }

    public BitcoinBillingMethod()
    {
        Id = Guid.NewGuid();
    }
}