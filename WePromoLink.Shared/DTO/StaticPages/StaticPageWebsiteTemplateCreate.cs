using Microsoft.AspNetCore.Http;

namespace WePromoLink.DTO.StaticPage;

public class StaticPageWebsiteTemplateCreate
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
}