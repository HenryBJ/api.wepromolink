using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IPaymentService
{
    Task<string> CreateInvoice(decimal amount, string firebaseId);
    Task HandleWebHook(HttpContext ctx);
}