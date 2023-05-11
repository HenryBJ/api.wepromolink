using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class StripeService
{
    private readonly DataContext _db;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public StripeService(DataContext db, IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }


    public async Task CreateUser(Subscription subscription)
    {
        try
        {
            if (subscription != null)
            {
                var productId = subscription.Items.Data[0].Price.ProductId;
                var subPlan = await _db.SubscriptionPlans.Where(e => e.AnnualyProductId == productId || e.MonthlyProductId == productId).SingleOrDefaultAsync();
                var service = new CustomerService();
                Customer customer = await service.GetAsync(subscription.CustomerId);


                var subService = new Stripe.SubscriptionService();
                var fullSub = await subService.GetAsync(subscription.Id);
                var lastInvoiceDate = fullSub.LatestInvoice?.PaymentIntent?.Created;
                var upcommigInvoiceDate = fullSub.NextPendingInvoiceItemInvoice;

                await _userService.Create(new User
                {
                    SubscriptionInfo = new SubscriptionInfo
                    {
                        Status = subscription.Status,
                        LastPaymentDate = lastInvoiceDate,
                        NextPaymentDate = upcommigInvoiceDate
                    },
                    Fullname = customer.Name,
                    Email = customer.Email,
                    CustomerId = customer.Id,
                    ProductId = productId,
                    SubscriptionPlanModelId = subPlan?.Id
                });
            }

        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public async Task UpdateUserSubscription(Subscription subscription, string status)
    {
        var subService = new Stripe.SubscriptionService();
        var fullSub = await subService.GetAsync(subscription.Id);
        var lastInvoiceDate = fullSub.LatestInvoice?.PaymentIntent?.Created;
        var upcommigInvoiceDate = fullSub.NextPendingInvoiceItemInvoice;

        if (!String.IsNullOrEmpty(subscription.CustomerId))
        {
            await _userService.UpdateSubscription(new SubscriptionInfo
            {
                Status = status,
                LastPaymentDate = lastInvoiceDate,
                NextPaymentDate = upcommigInvoiceDate
            }, subscription.CustomerId);
        }
        else
        {
            Console.WriteLine("Warning: ProductId is null");
        }
    }
}