namespace WePromoLink.Services;

public interface IPaymentService
{
    Task HandleWebHook(HttpContext ctx);
}