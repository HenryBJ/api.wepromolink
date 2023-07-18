namespace WePromoLink.DTO;

public class CampaignDetail
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public ImageData? ImageBundle { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public bool Status { get; set; }
    public decimal Budget { get; set; }
    public decimal EPM { get; set; }
    public DateTime? LastClick { get; set; }
    public DateTime? LastShared { get; set; }
}