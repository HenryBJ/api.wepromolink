using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Services;

public class UserService : IUserService
{
    private readonly DataContext _db;
    private readonly ILogger<UserService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailSender _emailSender;
    private readonly MessageBroker<BaseEvent> _eventSender;
    private readonly MessageBroker<StatsBaseCommand> _statSender;
    public UserService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger, IEmailSender emailSender, MessageBroker<BaseEvent> eventSender, MessageBroker<StatsBaseCommand> statSender)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _emailSender = emailSender;
        _eventSender = eventSender;
        _statSender = statSender;
    }

    public async Task BlockUser(string firebaseId, string reason)
    {
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");
        user.IsBlocked = true;
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
        _eventSender.Send(new UserBlockedEvent
        {
            Email = user.Email,
            Name = user.Fullname,
            Reason = reason,
            UserId = user.Id
        });
    }

    public async Task Create(User user, bool isSubscribed = false, string firebaseId = "")
    {
        var newUser = new UserModel
        {
            Id = Guid.NewGuid(),
            Email = user.Email,
            Fullname = user.Fullname,
            IsBlocked = false,
            IsSubscribed = isSubscribed,
            ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
            CreatedAt = DateTime.UtcNow,
            Profit = 0,
            Available = 0,
            Budget = 0,
            ThumbnailImageUrl = user.PhotoUrl ?? "",
            CustomerId = user.CustomerId,
            BitcoinBillingMethod = new BitcoinBillingMethod(),
            StripeBillingMethod = new StripeBillingMethod(),
            FirebaseId = firebaseId,
            AffiliateProgram = new AffiliateModel(),
            Privacy = new PrivacyModel(),
            Setting = new SettingModel(),
            Profile = new ProfileModel(),
            MyPage = new MyPageModel(),
            AffiliatedUsers = new List<AffiliatedUserModel>(),

            Subscription = new SubscriptionModel
            {
                StripeId = user.SubscriptionInfo.SubscriptionId,
                CreatedAt = DateTime.UtcNow,
                ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                Id = Guid.NewGuid(),
                LastPayment = user.SubscriptionInfo.LastPaymentDate,
                NextPayment = user.SubscriptionInfo.NextPaymentDate,
                Status = user.SubscriptionInfo.Status,
                IsCanceled = false,
                IsExpired = false,
                SubscriptionPlanModelId = user.SubscriptionPlanModelId!.Value
            },
        };
        await _db.Users.AddAsync(newUser);
        await _db.SaveChangesAsync();
        _eventSender.Send(new UserRegisteredEvent { UserId = newUser.Id, Name = newUser.Fullname, Email = newUser.Email });
    }

    public async Task Deposit(PaymentTransaction payment)
    {
        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                // Completamos la transaccion
                payment.CompletedAt = DateTime.UtcNow;
                payment.Status = TransactionStatusEnum.Completed;
                _db.PaymentTransactions.Update(payment);
                await _db.SaveChangesAsync();

                var user = await _db.Users
                .Include(e => e.Subscription)
                .ThenInclude(e => e.SubscriptionPlan)
                .Where(e => e.Id == payment.UserModelId)
                .SingleOrDefaultAsync();

                if (user == null) throw new Exception("User not found");
                decimal depositFee = user.Subscription.SubscriptionPlan.DepositFeePercent;
                decimal feeAmount = payment.Amount * (100 - depositFee) / 100;

                user.Available += feeAmount;
                _db.Users.Update(user);


                if (depositFee > 0)
                {
                    var paymentFee = new PaymentTransaction
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        UserModelId = user.Id,
                        CompletedAt = DateTime.UtcNow,
                        ExternalId = Nanoid.Nanoid.Generate(size: 12),
                        Status = TransactionStatusEnum.Completed,
                        Title = "Deposit Fee",
                        Amount = -payment.Amount * depositFee / 100,
                        TransactionType = TransactionTypeEnum.Fee,
                        AmountNet = -payment.Amount * depositFee / 100,
                    };
                    _db.PaymentTransactions.Add(paymentFee);
                }

                await _db.SaveChangesAsync();
                transaction.Commit();

                _statSender.Send(new AddAvailableCommand { ExternalId = user.ExternalId, Available = feeAmount });

                _eventSender.Send(new DepositCompletedEvent
                {
                    Amount = feeAmount,
                    PaymentTransactionId = payment.Id,
                    UserId = payment.UserModelId ?? Guid.Empty,
                    Name = payment.User?.Fullname,
                    Email = payment.User?.Email,
                    Fee = Math.Abs(payment.Amount * depositFee / 100)
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
            }
        }
    }

    public async Task<decimal> DepositFee()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        return await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .Include(e => e.Subscription)
        .ThenInclude(e => e.SubscriptionPlan)
        .Select(e => e.Subscription.SubscriptionPlan.DepositFeePercent).FirstAsync();
    }

    public async Task<bool> Exits(string email)
    {
        return await _db.Users.AnyAsync(e => e.Email.ToLower() == email.ToLower());
    }

    public async Task<string> GetExternalId()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        return await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.ExternalId).FirstAsync();
    }

    public async Task<int> GetLevel()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        return await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .Include(e => e.Subscription)
        .ThenInclude(e => e.SubscriptionPlan)
        .Select(e => e.Subscription.SubscriptionPlan.Level).FirstAsync();
    }

    public async Task<PaymentMethodData[]> GetPaymentMethods()
    {
        List<PaymentMethodData> result = new List<PaymentMethodData>();
        // Get UserId
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = _db.Users
        .Include(e => e.StripeBillingMethod)
        .Include(e => e.BitcoinBillingMethod)
        .Include(e => e.Subscription)
        .ThenInclude(e => e.SubscriptionPlan)
        .Where(e => e.FirebaseId == firebaseId).Single();

        var methods = user.Subscription.SubscriptionPlan.PaymentMethod.Split(',');

        foreach (var item in methods)
        {
            if (item.Trim().ToLower() == "bitcoin")
            {
                result.Add(new PaymentMethodData
                {
                    Name = "Bitcoin",
                    Value = user.BitcoinBillingMethod.Address,
                    IsVerified = user.BitcoinBillingMethod.IsVerified
                });
            }
            else
            if (item.Trim().ToLower() == "stripe")
            {
                result.Add(new PaymentMethodData
                {
                    Name = "Stripe",
                    Value = user.StripeBillingMethod.AccountId,
                    IsVerified = user.StripeBillingMethod.IsVerified
                });
            }
        }
        return result.ToArray();
    }

    public async Task<bool> IsBlocked()
    {
        bool result = false;
        var fireUser = await FirebaseUtil.GetUser(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.Email.ToLower() == fireUser.Email.ToLower()).SingleOrDefaultAsync();

        if (fireUser != null)
        {
            if (fireUser.Disabled)
            {
                if (user != null)
                {
                    user.IsBlocked = true;
                    await _db.SaveChangesAsync();
                }
                return true;
            }
        }
        if (user != null) return user.IsBlocked;
        return result;
    }

    public async Task<bool> IsSubscribed()
    {
        try
        {
            bool result = false;
            var fireUser = await FirebaseUtil.GetUser(_httpContextAccessor);
            var user = await _db.Users.Where(e => e.Email.ToLower() == fireUser.Email.ToLower()).SingleOrDefaultAsync();

            if (user != null) return user.IsSubscribed;
            return result;

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }

    }

    public async Task SetUserExtraInfo(string email, string uid, string photourl)
    {
        var user = await _db.Users.Where(e => e.Email.ToLower() == email.ToLower()).SingleOrDefaultAsync();
        if (user != null && String.IsNullOrEmpty(user.FirebaseId))
        {
            user.FirebaseId = uid;
            user.ThumbnailImageUrl = photourl;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> SignUp(SignUpData data)
    {
        try
        {
            var emailExits = await _db.Users.AnyAsync(e => e.Email.ToLower() == data.Email.ToLower());
            if (emailExits) return false; // Email already registered

            SubscriptionPlanModel plan = await _db.SubscriptionPlans
            .Where(e => e.ExternalId == data.SubscriptionPlanId)
            .SingleOrDefaultAsync();

            if (plan == null) return false;

            await this.Create(new User
            {
                PhotoUrl = data.PhotoUrl ?? "",
                ProductId = "",
                CustomerId = await CreateCustomer(new CustomerCreateOptions
                {
                    Email = data.Email,
                    Name = data.Fullname,
                }),
                Email = data.Email,
                Fullname = data.Fullname,
                SubscriptionPlanModelId = plan.Id,
                SubscriptionInfo = new SubscriptionInfo
                {
                    SubscriptionId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = "active"
                }
            }, true, data.FirebaseId);



            return true; //Ok
        }
        catch (System.Exception)
        {
            return false;
        }

    }

    private async Task<string> CreateCustomer(CustomerCreateOptions options)
    {
        var service = new CustomerService();
        var customer = await service.CreateAsync(options);
        return customer.Id;
    }

    public async Task UpdateSubscription(SubscriptionInfo subscriptionInfo, string CustomerId)
    {
        // find user
        var user = await _db.Users
        .Where(e => e.CustomerId == CustomerId)
        .Include(e => e.Subscription)
        .ThenInclude(e => e.SubscriptionPlan)
        .SingleOrDefaultAsync();

        if (user == null)
        {
            _logger.LogWarning($"Warning: Updating Subscription from Stripe, user not found, Subscription Status: {subscriptionInfo.Status}");
            return;
        }

        var sub = user!.Subscription;
        //incomplete, incomplete_expired, trialing, active, past_due, canceled, or unpaid
        switch (subscriptionInfo.Status)
        {
            case "past_due":
            case "unpaid":
            case "incomplete_expired":
            case "canceled":
            case "incomplete":
                sub.NextPayment = subscriptionInfo.NextPaymentDate;
                sub.LastPayment = subscriptionInfo.LastPaymentDate;
                sub.CanceledAt = DateTime.UtcNow;
                sub.IsCanceled = true;
                sub.IsExpired = true;
                sub.Status = subscriptionInfo.Status;
                user.IsSubscribed = false;
                SendEvent(user, subscriptionInfo.Status);
                break;

            case "trialing":
            case "active":
                sub.NextPayment = subscriptionInfo.NextPaymentDate;
                sub.LastPayment = subscriptionInfo.LastPaymentDate;
                sub.IsCanceled = false;
                sub.IsExpired = false;
                sub.Status = subscriptionInfo.Status;
                user.IsSubscribed = true;
                break;

            default:
                break;
        }

        _db.Subscriptions.Update(sub);
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task<decimal> WithdrawFee()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        return await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .Include(e => e.Subscription)
        .ThenInclude(e => e.SubscriptionPlan)
        .Select(e => e.Subscription.SubscriptionPlan.WithdrawFeePercent).FirstAsync();
    }

    private void SendEvent(UserModel user, string status)
    {
        _eventSender.Send(new UserSubscriptionExpiredEvent
        {
            Name = user.Fullname,
            UserId = user.Id,
            Subscription = user.SubscriptionModelId,
            SubscriptionPlanName = user.Subscription.SubscriptionPlan.Title
        });
    }

    public async Task Upgrade(string custumerId, Guid SubscriptionId)
    {
        var user = _db.Users
        .Include(e=>e.Subscription)
        .Where(e=>e.CustomerId == custumerId).First();
        
        _db.Subscriptions.Remove(user.Subscription);
        user.SubscriptionModelId = SubscriptionId;
        _db.Users.Update(user);
        await _db.SaveChangesAsync().ConfigureAwait(false);
    }
}