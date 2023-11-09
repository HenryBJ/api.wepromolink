namespace WePromoLink.DTO.StaticPage;

public class StaticPageCreate
{
    public string Name { get; set; }
    public string SLD { get; set; } //wepromolink
    public string TLD { get; set; } //com
    public string IP { get; set; }
    public Guid DataTemplateId { get; set; }
    public Guid WebsiteTemplateId { get; set; }
}