using Microsoft.AspNetCore.Http;

namespace WePromoLink.DTO.StaticPage;

public class StaticPageWebsiteTemplateRead
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string WebsiteTemplateUrl { get; set; }
}