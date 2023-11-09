namespace WePromoLink.Models;

public class StaticPageDataTemplateModel:StatsBaseModel 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Json { get; set; }
}