namespace WePromoLink.Models;

public class StaticPageWebsiteTemplateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Etag { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime LastModified { get; set; }
    public TimeSpan MaxAge { get; set; }
    public DateTime CreatedAt { get; set; }
}