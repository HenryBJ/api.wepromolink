using System.Net;

namespace WePromoLink;

public class HitAffiliate
{
    public string? AffLinkId { get; set; }
    public IPAddress? Origin { get; set; }
    public DateTime? HitAt { get; set; }
}