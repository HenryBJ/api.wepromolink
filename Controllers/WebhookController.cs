using Microsoft.AspNetCore.Mvc;
using Stripe;
using WePromoLink.Data;
using WePromoLink.Services;
using WePromoLink.Validators;

namespace WePromoLink.Controllers;

[ApiController]
[Route("[controller]")]
public class WebhookController : ControllerBase
{

    private readonly WebHookEventQueue _queue;

    // This is your Stripe CLI webhook secret for testing your endpoint locally.
    const string endpointSecret = "whsec_910a6037215b9b45b6e222ab692bae8058e9d04132471d858f40889e87da8921";

    public WebhookController(WebHookEventQueue queue)
    {
        _queue = queue;
    }



    [HttpPost]
    [Route("stripe")]
    public async Task<IResult> Stripe()
    {
        //  For testing:
        //  stripe login or stripe login --api-key sk_test_51Mvp......GM3RL
        //  ./stripe listen --forward-to localhost:5208/webhook/stripe
        //  stripe trigger payment_intent.succeeded
        //  stripe trigger --help

        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);

            _queue.Item = stripeEvent;

            return Results.Ok();
        }
        catch (StripeException e)
        {
            return Results.BadRequest();
        }
    }
}