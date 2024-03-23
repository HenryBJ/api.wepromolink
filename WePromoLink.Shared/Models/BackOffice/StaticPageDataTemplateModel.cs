namespace WePromoLink.Models;

public class StaticPageDataTemplateModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Json { get; set; }
    public string Etag { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime LastModified { get; set; }
    public TimeSpan MaxAge { get; set; }
    public DateTime CreatedAt { get; set; }
}