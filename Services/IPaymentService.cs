using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IPaymentService
{
    Task<string> CreateInvoice(PaymentTransaction payment, string redirectUrl = "");
    Task HandleWebHook(HttpContext ctx);
}