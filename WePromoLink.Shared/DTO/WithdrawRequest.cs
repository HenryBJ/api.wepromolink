using WePromoLink.DTO;

namespace WePromoLink;

public class WithdrawRequest : Transaction
{
    public string UserImageUrl { get; set; }
    public string UserName { get; set; }
    public decimal Available { get; set; }
}