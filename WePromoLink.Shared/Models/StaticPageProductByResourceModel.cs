namespace WePromoLink.Models;

public class StaticPageProductByResourceModel
{
    public Guid Id { get; set; }
    public Guid? StaticPageProductModelId { get; set; }
    public StaticPageProductModel? Product { get; set; }
    public Guid? StaticPageResourceModelId { get; set; }
    public StaticPageResourceModel? Resource { get; set; }
    
}