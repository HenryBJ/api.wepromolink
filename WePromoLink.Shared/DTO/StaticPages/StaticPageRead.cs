namespace WePromoLink.DTO.StaticPage;

public class StaticPageRead
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IP { get; set; }
    public Guid DataTemplateId { get; set; }
    public string DataTemplateName { get; set; }
    public string DataTemplateUrl { get; set; }
    public Guid WebsiteTemplateId { get; set; }
    public string WebsiteTemplateName { get; set; }
    public string WebsiteTemplateUrl { get; set; }
}