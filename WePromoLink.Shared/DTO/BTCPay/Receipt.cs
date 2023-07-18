using System.Text.Json.Serialization; 
namespace WePromoLink.DTO.BTCPay{ 

    public class Receipt
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("showQR")]
        public object ShowQR { get; set; }

        [JsonPropertyName("showPayments")]
        public object ShowPayments { get; set; }
    }

}