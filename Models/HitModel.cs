using WePromoLink.Models;

namespace WePromoLink;

public class HitModel
{
    public Guid Id { get; set; }
    public LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
    public string? Origin { get; set; }
    public string? Geolocation { get; set; }
    public bool IsGeolocated { get; set; }
    public string? MapImageUrl { get; set; }
    public DateTime? FirstHitAt { get; set; }
    public DateTime? LastHitAt { get; set; }
    public int Counter { get; set; }
    public string? Country { get; set; }
    public DateTime? GeolocatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    
}