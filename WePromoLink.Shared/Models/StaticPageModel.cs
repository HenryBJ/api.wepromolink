namespace WePromoLink.Models;

public class StaticPageModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IP { get; set; }
    public Guid StaticPageDataTemplateModelId { get; set; }
    public StaticPageDataTemplateModel StaticPageDataTemplate { get; set; }
    public Guid StaticPageWebsiteTemplateModelId { get; set; }
    public StaticPageWebsiteTemplateModel StaticPageWebsiteTemplate { get; set; }
    public string Etag { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime LastModified { get; set; }
    public TimeSpan MaxAge { get; set; }
    public DateTime CreatedAt { get; set; }
}