using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class PricingService : IPricingService
{
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public PricingService(DataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PricingCard[]> GetAll()
    {
        var isAuth = _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Wepromolink-UserId", out StringValues userId);
        int level = 99;
        string planId ="";
        if (isAuth.HasValue && isAuth.Value)
        {
            var firebaseId = userId[0];
            var data = _db.Users
            .Include(e => e.Subscription)
            .ThenInclude(e => e.SubscriptionPlan)
            .Where(e => e.FirebaseId == firebaseId)
            .Select(e => new
            {
                level = e.Subscription.SubscriptionPlan.Level,
                planId = e.Subscription.SubscriptionPlan.ExternalId
            }).First();
            level = data.level;
            planId = data.planId;
        }

        return await _db.SubscriptionPlans
        .Include(e => e.Features)
        .Where(e => e.Active)
        .Select(e => new PricingCard
        {
            Id = e.ExternalId,
            Annually = e.Annually,
            Monthly = e.Monthly,
            Discount = e.Discount,
            PaymentMethod = e.PaymentMethod,
            Title = e.Title,
            Tag = e.Tag,
            AnnualyPriceId = e.AnnualyPriceId,
            Commission = e.Commission,
            MonthlyPriceId = e.MonthlyPriceId,
            Order = e.Order,
            Disabled = e.ExternalId == planId || (level<99 && e.Level<level),
            Upgradeable = e.Level>level,
            Features = e.Features.Select(k => new PricingFeature { Order = k.Order, BoolValue = k.BoolValue, Name = k.Name, Value = k.Value }).ToList()

        }).ToArrayAsync();
    }
}