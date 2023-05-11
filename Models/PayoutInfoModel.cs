namespace WePromoLink.Models;

public class PayoutInfoModel
{
    public Guid Id { get; set; }
    public Guid UserModelId { get; set; }
    public virtual UserModel User { get; set; }
    public string PayoutType { get; set; } // bitcoin, paypal, mastercard, visa, stripe, wire
    public string? BCTAddress { get; set; }
    public string? DebitCard { get; set; }
    public string? Paypal { get; set; }
    public string? Stripe { get; set; }
    public string? WireName { get; set; }
    public string? WireBankName { get; set; }
    public string? WireSwiftorBic { get; set; }
    public string? WireIban { get; set; }
    public string? WireBankAddress { get; set; }
    public string? WireBranch { get; set; }
    public string? wireRouting { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }

}