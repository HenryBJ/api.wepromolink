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

public class UserService : IUserService
{
    private readonly DataContext _db;
    private readonly ILogger<UserService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
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
            ThumbnailImageUrl = "",
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
            HistoryClicksOnCampaignUser = new HistoryClicksOnCampaignUserModel(),
            HistoryClicksOnLinksUser = new HistoryClicksOnLinksUserModel(),
            HistoryClicksOnSharesUser = new HistoryClicksOnSharesUserModel(),
            HistoryEarnByCountriesUser = new HistoryEarnByCountriesUserModel(),
            HistoryEarnOnLinksUser = new HistoryEarnOnLinksUserModel(),
            HistorySharedByUsersUser = new HistorySharedByUsersUserModel(),
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

    public async Task<bool> Exits(string email)
    {
        return await _db.Users.AnyAsync(e => e.Email.ToLower() == email.ToLower());
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