namespace WePromoLink.DTO;

public class LinkDetail
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public decimal Profit { get; set; }
    public decimal Epm { get; set; }
    public Boolean Status { get; set; }
    public DateTime? LastClick { get; set; }
}