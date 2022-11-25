using WePromoLink.Models;

namespace WePromoLink;

public class HitAffiliateModel
{
    public int Id { get; set; }
    public AffiliateLinkModel AffiliateLink { get; set; }
    public int AffiliateLinkModelId { get; set; }
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