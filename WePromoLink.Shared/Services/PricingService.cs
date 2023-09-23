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
        return await _db.SubscriptionPlans.Include(e => e.Features).Select(e => new PricingCard
        {
            Id = e.ExternalId,
            Annually = e.Annually,
            Monthly = e.Monthly,
            Discount = e.Discount,
            PaymentMethod = e.PaymentMethod,
            Title = e.Title,
            Tag = e.Tag,
            MonthlyPaymantLink = e.MonthlyPaymantLink,
            AnnualyPaymantLink = e.AnnualyPaymantLink,
            Order = e.Order,
            Features = e.Features.Select(k => new PricingFeature { BoolValue = k.BoolValue, Name = k.Name, Value = k.Value }).ToList()

        }).ToArrayAsync();
    }
}