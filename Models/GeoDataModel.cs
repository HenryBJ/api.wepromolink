namespace WePromoLink.Models;

public class GeoDataModel
{
    public Guid Id { get; set; }
    public string? IP { get; set; }
    public string? Country { get; set; }
    public string? Continent { get; set; }
    public string? CountryCode { get; set; }
    public string? Region { get; set; }
    public string? RegionCode { get; set; }
    public string? City { get; set; }
    public string? Zip { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? CountryFlagUrl { get; set; }
    public string? Currency { get; set; }
    public string? ISP { get; set; }


}