using Microsoft.AspNetCore.Http;

namespace WePromoLink.DTO.StaticPage;

public class StaticPageWebsiteTemplateEdit
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IFormFile File { get; set; }
}