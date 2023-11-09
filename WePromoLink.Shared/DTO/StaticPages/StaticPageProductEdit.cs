namespace WePromoLink.DTO.StaticPage;

public class StaticPageProductEdit
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public int Inventory { get; set; }
    public string SKU { get; set; }
    public string Provider { get; set; }
    public string Category { get; set; }
    public string Tags { get; set; }
    public decimal? Discount { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public string? AffiliateProgram { get; set; }
    public decimal? Commission { get; set; }
    public string? AffiliateLink { get; set; }
    public string? BuyLink { get; set; }
}