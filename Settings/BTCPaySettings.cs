using System.Net.Http.Headers;
using System.Text;

namespace WePromoLink.Settings;

public class BTCPaySettings
{
    public string StoreId { get; set; }
    public string Secret { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string APIKey { get; set; }
    public string Url { get; set; }
    public string ReturnUrl { get; set; }
    public string BTCAddress { get; set; }
    public decimal Fee { get; set; }

}