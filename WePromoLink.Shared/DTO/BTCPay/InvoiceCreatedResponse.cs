using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace WePromoLink.DTO.BTCPay{ 

    public class InvoiceCreatedResponse
    {
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("checkout")]
        public Checkout Checkout { get; set; }

        [JsonPropertyName("receipt")]
        public Receipt Receipt { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("storeId")]
        public string StoreId { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("checkoutLink")]
        public string CheckoutLink { get; set; }

        [JsonPropertyName("createdTime")]
        public int CreatedTime { get; set; }

        [JsonPropertyName("expirationTime")]
        public int ExpirationTime { get; set; }

        [JsonPropertyName("monitoringTime")]
        public int MonitoringTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("additionalStatus")]
        public string AdditionalStatus { get; set; }

        [JsonPropertyName("availableStatusesForManualMarking")]
        public List<string> AvailableStatusesForManualMarking { get; set; }

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }
    }

}