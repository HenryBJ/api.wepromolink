using Microsoft.AspNetCore.Http;

namespace WePromoLink.DTO.StaticPage;

public class StaticPageDataTemplateRead
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string DataTemplateJsonUrl { get; set; }
}