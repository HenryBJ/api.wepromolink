using WePromoLink.DTO;

namespace WePromoLink;

public class MyLink
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public ImageData? ImageData { get; set; }
    public decimal Profit { get; set; }
    public bool Status { get; set; }
    public DateTime? LastClick { get; set; }

}