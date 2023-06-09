using System.Net;

namespace WePromoLink;

public class Hit
{
    public string? LinkId { get; set; }
    public IPAddress? Origin { get; set; }
    public DateTime? HitAt { get; set; }
}