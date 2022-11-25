using System.Text.Json.Serialization;

namespace WePromoLink.DTO.BTCPay;

public class Payment
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("receivedDate")]
    public int ReceivedDate { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("fee")]
    public string Fee { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("destination")]
    public string Destination { get; set; }
}