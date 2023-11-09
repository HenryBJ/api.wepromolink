namespace WePromoLink.Models;

public class StaticPageModel:StatsBaseModel 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IP { get; set; }
    public Guid StaticPageDataTemplateModelId { get; set; }
    public StaticPageDataTemplateModel StaticPageDataTemplate { get; set; }
    public Guid StaticPageWebsiteTemplateModelId { get; set; }
    public StaticPageWebsiteTemplateModel StaticPageWebsiteTemplate { get; set; }
}