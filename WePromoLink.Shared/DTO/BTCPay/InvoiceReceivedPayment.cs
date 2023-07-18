using System.Text.Json.Serialization;

namespace WePromoLink.DTO.BTCPay;

public class InvoiceReceivedPayment: BTCPayEventBase
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

    [JsonPropertyName("afterExpiration")]
    public bool AfterExpiration { get; set; }

    [JsonPropertyName("paymentMethod")]
    public string PaymentMethod { get; set; }

    [JsonPropertyName("payment")]
    public Payment Payment { get; set; }
}