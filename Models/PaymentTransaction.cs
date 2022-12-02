using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WePromoLink.Models;

public class PaymentTransaction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int? AffiliateLinkId { get; set; }
    public int? SponsoredLinkId { get; set; }
    public decimal Amount { get; set; }
    public bool IsDeposit { get; set; }
    public int? EmailModelId { get; set; }
    public string Status { get; set; }
    public string? PaymentLink { get; set; }
    public string? PayoutId { get; set; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime CreatedAt { get; set; }
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? ExpiredAt { get; set; }
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? CompletedAt { get; set; }
}