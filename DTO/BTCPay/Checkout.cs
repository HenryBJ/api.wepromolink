using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace WePromoLink.DTO.BTCPay{ 

    public class Checkout
    {
        [JsonPropertyName("speedPolicy")]
        public string SpeedPolicy { get; set; } = "MediumSpeed";

        [JsonPropertyName("paymentMethods")]
        public List<string> PaymentMethods { get; set; }

        [JsonPropertyName("defaultPaymentMethod")]
        public string DefaultPaymentMethod { get; set; }

        [JsonPropertyName("expirationMinutes")]
        public int ExpirationMinutes { get; set; }

        [JsonPropertyName("monitoringMinutes")]
        public int MonitoringMinutes { get; set; }

        [JsonPropertyName("paymentTolerance")]
        public double PaymentTolerance { get; set; }

        [JsonPropertyName("redirectURL")]
        public string RedirectURL { get; set; }

        [JsonPropertyName("redirectAutomatically")]
        public bool RedirectAutomatically { get; set; }

        [JsonPropertyName("requiresRefundEmail")]
        public bool RequiresRefundEmail { get; set; }

        [JsonPropertyName("defaultLanguage")]
        public string DefaultLanguage { get; set; }
    }

}