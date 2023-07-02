using System.Dynamic;
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

public class UserService : IUserService
{
    private readonly DataContext _db;
    private readonly ILogger<UserService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmailSender _emailSender;
    public UserService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger, IEmailSender emailSender)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _emailSender = emailSender;
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
            ThumbnailImageUrl = user.PhotoUrl ?? "",
            CustomerId = user.CustomerId,
            Available = new AvailableModel(),
            Budget = new BudgetModel(),
            SharedToday = new SharedTodayUserModel(),
            SharedLastWeek = new SharedLastWeekUserModel(),
            ClickLastWeekOnLinksUser = new ClickLastWeekOnLinksUserModel(),
            ClicksLastWeekOnCampaignUser = new ClicksLastWeekOnCampaignUserModel(),
            ClicksTodayOnCampaignUser = new ClicksTodayOnCampaignUserModel(),
            ClicksTodayOnLinksUser = new ClicksTodayOnLinksUserModel(),
            EarnLastWeekUser = new EarnLastWeekUserModel(),
            EarnTodayUser = new EarnTodayUserModel(),
            HistoryClicksByCountriesOnCampaignUser = new HistoryClicksByCountriesOnCampaignUserModel(),
            HistoryClicksByCountriesOnLinkUser = new HistoryClicksByCountriesOnLinkUserModel(),
            HistoryClicksOnCampaignUser = new HistoryClicksOnCampaignUserModel(), //done
            HistoryClicksOnLinksUser = new HistoryClicksOnLinksUserModel(), // done
            HistoryClicksOnSharesUser = new HistoryClicksOnSharesUserModel(), //done
            HistoryEarnByCountriesUser = new HistoryEarnByCountriesUserModel(),
            HistoryEarnOnLinksUser = new HistoryEarnOnLinksUserModel(), //done
            HistorySharedByUsersUser = new HistorySharedByUsersUserModel(),
            BitcoinBillingMethod = new BitcoinBillingMethod(),
            StripeBillingMethod = new StripeBillingMethod(),
            Locked = new LockedModel(),
            PayoutStat = new PayoutStatModel(),
            Profit = new ProfitModel(),
            FirebaseId = firebaseId,


            Subscription = new SubscriptionModel
            {
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

                // Asignamos la cantidad al Available
                var available = await _db.Availables
                .Where(e => e.UserModelId == payment.UserModelId)
                .SingleOrDefaultAsync();
                if (available == null) throw new Exception("No Availabe account found");

                available.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                available.Value += payment.Amount;
                _db.Availables.Update(available);
                await _db.SaveChangesAsync();

                // Notificamos del Deposito
                var noti = new NotificationModel
                {
                    Id = Guid.NewGuid(),
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = NotificationStatusEnum.Unread,
                    UserModelId = payment.UserModelId!.Value,
                    Title = "Deposit completed",
                    Message = $"We are pleased to inform you that your Bitcoin deposit has been successfully processed. An amount of $${payment.Amount} USD has been credited to your account.",
                };
                await _db.Notifications.AddAsync(noti);
                await _db.SaveChangesAsync();

                // Enviamos un correo
                var user = await _db.Users.Where(e => e.Id == payment.UserModelId).SingleOrDefaultAsync();
                if (user == null) throw new Exception("User no found");
                await _emailSender.Send(user.Fullname!, user.Email, "Deposit completed", Templates.DepositBTC(new { user = user.Fullname, amount = payment.Amount.ToString("C") }));

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
            }
        }
    }

    public async Task<bool> Exits(string email)
    {
        return await _db.Users.AnyAsync(e => e.Email.ToLower() == email.ToLower());
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

    public async Task SetFirebaseUid(string email, string Uid)
    {
        var user = await _db.Users.Where(e => e.Email.ToLower() == email.ToLower()).SingleOrDefaultAsync();
        if (user != null && String.IsNullOrEmpty(user.FirebaseId))
        {
            user.FirebaseId = Uid;
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

            if (plan == null) return false; /// Invalid Plan

            await this.Create(new User
            {
                PhotoUrl = data.PhotoUrl ?? "",
                ProductId = "",
                CustomerId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                Email = data.Email,
                Fullname = data.Fullname,
                SubscriptionPlanModelId = plan.Id,
                SubscriptionInfo = new SubscriptionInfo
                {
                    Status = "active"
                }
            }, true, data.FirebaseId);

            // Notificar por email (Welcome)
            await _emailSender.Send(data.Fullname, data.Email, "Welcome to WePromoLink", Templates.Welcome(new { user = data.Fullname }));


            return true; //Ok
        }
        catch (System.Exception)
        {
            return false;
        }

    }

    public async Task UpdateSubscription(SubscriptionInfo subscriptionInfo, string CustomerId)
    {
        // find user
        var user = await _db.Users.Where(e => e.CustomerId == CustomerId).Include(e => e.Subscription).SingleOrDefaultAsync();
        if (user == null)
        {
            Console.WriteLine("Warning: Upddating Subscription from Stripe, user not found");
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
}