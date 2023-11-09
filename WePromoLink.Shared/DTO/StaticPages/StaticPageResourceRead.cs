namespace WePromoLink.DTO.StaticPage;

public class StaticPageResourceRead
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public decimal SizeMB { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
    public string Url { get; set; }
}