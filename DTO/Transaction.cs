namespace WePromoLink;

public class Transaction
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}