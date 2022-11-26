namespace WePromoLink.Models;

public class SponsoredLinkModel
{
    public int Id { get; set; }
    public string ExternalId { get; set; }
    public EmailModel Email { get; set; }
    public int EmailModelId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Url { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Budget { get; set; }
    public decimal RemainBudget { get; set; }
    public decimal EPM { get; set; }
    public DateTime? LastClick { get; set; }
    public DateTime? LastShared { get; set; }
    public DateTime? LastUpdated { get; set; }
    public DateTime CreatedAt { get; set; }
}