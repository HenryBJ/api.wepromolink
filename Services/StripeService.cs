using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class StripeService
{
    private readonly DataContext _db;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ILogger<StripeService> _logger { get; set; }
    public StripeService(DataContext db, IHttpContextAccessor httpContextAccessor, IUserService userService, ILogger<StripeService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
        _logger = logger;
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
            _logger.LogError(ex.Message);
        }

    }

    public async Task VerifyAccount(string accountId)
    {
        if (string.IsNullOrEmpty(accountId)) return;
        var billing = await _db.StripeBillings.Where(e => e.AccountId == accountId).SingleOrDefaultAsync();
        if (billing == null)
        {
            _logger.LogWarning($"Account ${accountId} not found in DB trying to verify it");
            return;
        }
        billing.LastModified = DateTime.UtcNow;
        billing.VerifiedAt = DateTime.UtcNow;
        billing.IsVerified = true;
        _db.StripeBillings.Update(billing);
        await _db.SaveChangesAsync();
    }

    public async Task<string> CreateAccountLink()
    {
        // Get UserId
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = _db.Users.Include(e => e.StripeBillingMethod).Where(e => e.FirebaseId == firebaseId).Single();

        if (string.IsNullOrEmpty(user.StripeBillingMethod.AccountId))
        {
            // Creating
            user.StripeBillingMethod.AccountId = await CreateStripeAccount(user.Email);
        }

        user.StripeBillingMethod.LastModified = DateTime.UtcNow;
        _db.StripeBillings.Update(user.StripeBillingMethod);
        await _db.SaveChangesAsync();

        var accLinkService = new Stripe.AccountLinkService();
        AccountLinkCreateOptions options = new AccountLinkCreateOptions
        {
            Account = user.StripeBillingMethod.AccountId,
            Type = "account_onboarding",
            ReturnUrl = "https://wepromolink.com/billing",
            RefreshUrl = "https://wepromolink.com/billing"
        };

        AccountLink response = await accLinkService.CreateAsync(options);
        return response.Url;
    }

    public async Task<bool> HasVerifiedAccount()
    {
        // Get User
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var hasVerified = await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .Select(e => e.StripeBillingMethod.IsVerified)
        .SingleAsync();
        return hasVerified;
    }

    public async Task<string> GetStripeDashboardLink()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var accountId = await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .Select(e => e.StripeBillingMethod.AccountId)
        .SingleAsync();

        var loginAccService = new Stripe.LoginLinkService();
        LoginLink loginLink = await loginAccService.CreateAsync(accountId);
        return loginLink.Url;
    }

    public async Task<string> CreateInvoice(int amount)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                int amountCents = amount * 100;
                // Get User
                var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
                var user = await _db.Users
                .Where(e => e.FirebaseId == firebaseId)
                .SingleAsync();

                var invoiceService = new Stripe.InvoiceService();
                var invoiceItemService = new Stripe.InvoiceItemService();
                var priceService = new Stripe.PriceService();

                // Create Payment Transaction
                var paymentTrans = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = TransactionStatusEnum.Pending,
                    Title = $"Deposit ${amount}",
                    TransactionType = TransactionTypeEnum.Deposit,
                    UserModelId = user.Id,
                    ExpiredAt = DateTime.UtcNow.AddDays(10)
                };
                await _db.PaymentTransactions.AddAsync(paymentTrans);
                await _db.SaveChangesAsync();

                // Create invoice
                var options = new InvoiceCreateOptions
                {
                    Currency = "usd",
                    CollectionMethod = "send_invoice",
                    Customer = user.CustomerId,
                    DaysUntilDue = 10,
                    Metadata = new Dictionary<string, string>
                    {
                        {"type", "deposit"},
                        {"firebaseId",user.FirebaseId??""},
                        {"paymentId",paymentTrans.ExternalId}
                    }
                };

                Invoice invoice = await invoiceService.CreateAsync(options);
                // Get price object
                var prices = await priceService.ListAsync();
                var selected_price = prices.Where(e => e.UnitAmount == amountCents).FirstOrDefault();
                if (selected_price == null) throw new Exception("Price info not found");

                // Create invoice item
                var invoiceItemOptions = new InvoiceItemCreateOptions
                {
                    Customer = user.CustomerId,
                    Quantity = 1,
                    Description = $"${amount} usd of credit",
                    Invoice = invoice.Id,
                    Price = selected_price.Id
                };
                var invoiceItem = await invoiceItemService.CreateAsync(invoiceItemOptions);

                // Finalize it
                invoice = await invoiceService.FinalizeInvoiceAsync(invoice.Id);

                //Send invoice
                await invoiceService.SendInvoiceAsync(invoice.Id);

                await transaction.CommitAsync();
                return invoice.HostedInvoiceUrl;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Creating invoice fail");
            }
        }
    }


    public async Task CreateWithdrawRequest(int amount)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                int amountCents = amount * 100;
                // Get User
                var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
                var user = await _db.Users
                .Include(e => e.Profit)
                .Where(e => e.FirebaseId == firebaseId)
                .SingleAsync();

                // Discount from Profit
                user.Profit.Value -= amount;
                user.Profit.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                _db.Profits.Update(user.Profit);

                if (user.Profit.Value < 0) throw new Exception("Profit negative");

                // Create Payment Transaction
                var paymentTrans = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    CreatedAt = DateTime.UtcNow,
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = TransactionStatusEnum.Requesting,
                    Title = $"Withdraw ${amount}",
                    TransactionType = TransactionTypeEnum.Withdraw,
                    UserModelId = user.Id,
                    ExpiredAt = DateTime.UtcNow.AddDays(10),
                    Metadata = "stripe"
                };
                await _db.PaymentTransactions.AddAsync(paymentTrans);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Creating payment transaction fail");
            }
        }
    }

    public async Task HandleInvoiceWebHook(Invoice invoice)
    {
        if (invoice.Metadata.ContainsKey("paymentId"))
        {
            var paymentId = invoice.Metadata["paymentId"];
            var pay = await _db.PaymentTransactions
            .Where(e => e.ExternalId == paymentId)
            .SingleOrDefaultAsync();
            if (pay == null) throw new Exception("Payment transaction not found");
            await _userService.Deposit(pay);
        }
        else
        {
            _logger.LogInformation("Receive invoice paid event without payment info, must be from subscription");
        }

    }

    private async Task<string> CreateStripeAccount(string email)
    {
        var accService = new Stripe.AccountService();
        AccountCreateOptions options = new AccountCreateOptions
        {
            Type = AccountType.Express,
            Email = email,
            BusinessType = "individual"
        };
        Account account = await accService.CreateAsync(options);
        return account.Id;
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