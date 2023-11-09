using Microsoft.AspNetCore.Http;

namespace WePromoLink.DTO.StaticPage;

public class StaticPageResourceEdit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public IFormFile File { get; set; }
    public decimal SizeMB { get; set; }
    public int? Height { get; set; }
    public int? Width { get; set; }
}