using System.Text.Json.Serialization; 
using System.Collections.Generic; 
namespace WePromoLink.DTO.BTCPay{ 

    public class InvoiceCreateOptions
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("checkout")]
        public Checkout Checkout { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("receipt")]
        public Receipt Receipt { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("additionalSearchTerms")]
        public List<string> AdditionalSearchTerms { get; set; }

        public InvoiceCreateOptions(int amount, string returnURL)
        {
            Amount = amount;
            Currency = "USD";
            Metadata = new Metadata();
            Checkout = new Checkout
            {
                SpeedPolicy = "MediumSpeed",
                ExpirationMinutes = 25,
                RedirectAutomatically = true,
                RedirectURL = returnURL
            };
            Receipt = new Receipt
            {
                Enabled = false
            };
        }
    }

}