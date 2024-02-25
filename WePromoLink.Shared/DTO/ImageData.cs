namespace WePromoLink.DTO;

public class ImageData
{
    public string ExternalId { get; set; }
    public string Original { get; set; }
    public int OriginalWidth { get; set; }
    public int OriginalHeight { get; set; }
    public double OriginalAspectRatio { get; set; }
    public string Compressed { get; set; }
    public int CompressedWidth { get; set; }
    public int CompressedHeight { get; set; }
    public double CompressedAspectRatio { get; set; }
    public string Medium { get; set; }
    public int MediumWidth { get; set; }
    public int MediumHeight { get; set; }
    public double MediumAspectRatio { get; set; }
    public string Thumbnail { get; set; }
    public int ThumbnailWidth { get; set; }
    public int ThumbnailHeight { get; set; }
    public double ThumbnailAspectRatio { get; set; }
    public DateTime CreatedAt { get; set; }
    
}