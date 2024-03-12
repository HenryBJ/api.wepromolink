using System.ComponentModel.DataAnnotations;

namespace WePromoLink.DTO;

public class CheckoutData
{
    public string PriceId { get; set; }
    public string FirebaseId { get; set; }
    public string PhotoUrl { get; set; }
}