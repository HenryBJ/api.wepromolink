using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WePromoLink.Models;

public class PaymentTransaction
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public LinkModel? Link { get; set; }
    public Guid? LinkModelId { get; set; }
    public UserModel? User { get; set; }
    public Guid? UserModelId { get; set; }
    public CampaignModel? Campaign { get; set; }
    public Guid? CampaignModelId { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } // See TransactionTypeEnum
    public string Status { get; set; } // See TransactionStatusEnum
    public string? Metadata { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime CreatedAt { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? ExpiredAt { get; set; }

    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime? CompletedAt { get; set; }
}