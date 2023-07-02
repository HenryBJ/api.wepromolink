namespace WePromoLink;

public class MyCampaign
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public bool Status { get; set; }
    public string? ImageBundleId { get; set; }
    public decimal Budget { get; set; }
    public decimal EPM { get; set; }
    public DateTime? LastClick { get; set; }
    public DateTime? LastShared { get; set; }
}