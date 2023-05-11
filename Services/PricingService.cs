using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    public PricingService(DataContext db)
    {
        _db = db;
    }

    public async Task<PricingCard[]> GetAll()
    {
        return await _db.SubscriptionPlans.Select(e => new PricingCard
        {
            Id = e.ExternalId,
            Ads = e.ContainAds,
            Annually = e.Annually,
            Monthly = e.Monthly,
            DepositFee = e.DepositFee,
            Discount = e.Discount,
            PaymentMethod = e.PaymentMethod,
            PayoutFee = e.PayoutFee,
            PayoutMinimun = e.PayoutMinimun,
            Title = e.Title,
            Tag = e.Tag,
            MonthlyPaymantLink = e.MonthlyPaymantLink,
            AnnualyPaymantLink = e.AnnualyPaymantLink,
            Order = e.Order
        }).ToArrayAsync();
    }
}