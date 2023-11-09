namespace WePromoLink.Models;

public class StaticPageProductByPageModel
{
    public Guid Id { get; set; }
    public Guid? StaticPageProductModelId { get; set; }
    public StaticPageProductModel? Product { get; set; }
    public Guid? StaticPageModelId { get; set; }
    public StaticPageModel? Page { get; set; }
    public int AffiliateClicks { get; set; }
    public int BuyClicks { get; set; }
    public decimal Profit { get; set; }
}