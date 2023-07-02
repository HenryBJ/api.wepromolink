namespace WePromoLink.DTO;

public class CampaignCard
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal EPM { get; set; }
    public ImageData? ImageBundle { get; set; }
    public string? AutorImageUrl { get; set; }
    public string? AutorName { get; set; }
    public long LastModified { get; set; }
    
}