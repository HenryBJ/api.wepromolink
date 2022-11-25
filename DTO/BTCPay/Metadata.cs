using System.Text.Json.Serialization; 
namespace WePromoLink.DTO.BTCPay{ 

    public class Metadata
    {
        public string UserId { get; set; }
        public string PlanId { get; set; }
        public string SubscriptionId { get; set; }
    }

}