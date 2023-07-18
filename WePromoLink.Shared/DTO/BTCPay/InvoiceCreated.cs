using System.Text.Json.Serialization;

namespace WePromoLink.DTO.BTCPay;

public abstract class BTCPayEventBase{}

public class InvoiceCreated: BTCPayEventBase
{
    [JsonPropertyName("deliveryId")]
    public string DeliveryId { get; set; }

    [JsonPropertyName("webhookId")]
    public string WebhookId { get; set; }

    [JsonPropertyName("originalDeliveryId")]
    public string OriginalDeliveryId { get; set; }

    [JsonPropertyName("isRedelivery")]
    public bool IsRedelivery { get; set; }

    [JsonPropertyName("type")]
    public string @Type { get; set; }

    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; }

    [JsonPropertyName("storeId")]
    public string StoreId { get; set; }

    [JsonPropertyName("invoiceId")]
    public string InvoiceId { get; set; }
}