using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WePromoLink.Data;
using WePromoLink.DTO.SubscriptionPlan;
using WePromoLink.Models;

namespace WePromoLink.Services.SubscriptionPlan;

public class SubPlanService : ISubPlanService
{
    private readonly DataContext _db;

    public SubPlanService(DataContext db)
    {
        _db = db;
    }

    public async Task<Guid> Create(SubscriptionPlanCreate subPlan)
    {
        SubscriptionPlanModel model = new SubscriptionPlanModel
        {
            Annually = subPlan.Annually,
            AnnualyPriceId = subPlan.AnnualyPriceId,
            MonthlyPriceId = subPlan.MonthlyPriceId,
            Commission = subPlan.Commission,            
            Discount = subPlan.Discount,
            ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
            Id = Guid.NewGuid(),
            Level = subPlan.Level,
            Monthly = subPlan.Monthly,
            Order = subPlan.Order,
            PaymentMethod = subPlan.PaymentMethod,
            Tag = subPlan.Tag,
            Title = subPlan.Title
        };
        _db.SubscriptionPlans.Add(model);
        foreach (var feature in subPlan.Features)
        {
            _db.SubscriptionFeatures.Add(new SubscriptionFeatureModel
            {
                Id = Guid.NewGuid(),
                BoolValue = feature.BoolValue,
                Name = feature.Name,
                Value = feature.Value,
                SubscriptionPlanModelId = model.Id
            });
        }
        _db.SaveChanges();
        return model.Id;
    }

    public async Task<Guid> Create(SubscriptionPlanFeatureCreate feature)
    {
        Guid Id = Guid.NewGuid();
        await _db.SubscriptionFeatures.AddAsync(new SubscriptionFeatureModel
        {
            Id = Id,
            BoolValue = feature.BoolValue,
            Name = feature.Name,
            Value = feature.Value,
            SubscriptionPlanModelId = feature.SubscrioptionPlanId.HasValue ? feature.SubscrioptionPlanId.Value : Guid.Empty
        });
        _db.SaveChanges();
        return Id;
    }

    public async Task Delete(SubscriptionPlanDelete subPlan)
    {
        var item = await _db.SubscriptionPlans.Where(e => e.Id == subPlan.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Subscription plan not found");
        _db.SubscriptionPlans.Remove(item);
        _db.SaveChanges();
    }

    public async Task Delete(SubscriptionPlanFeatureDelete feature)
    {
        var item = await _db.SubscriptionFeatures.Where(e => e.Id == feature.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("subscription plan feature not found");

        _db.SubscriptionFeatures.Remove(item);
        _db.SaveChanges();
    }

    public async Task Edit(SubscriptionPlanEdit subPlan)
    {
        var item = await _db.SubscriptionPlans.Where(e => e.Id == subPlan.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Subscription plan not found");

        item.Annually = subPlan.Annually;
        item.Discount = subPlan.Discount;
        item.Monthly = subPlan.Monthly;
        item.Order = subPlan.Order;
        item.AnnualyPriceId = subPlan.AnnualyPriceId;
        item.MonthlyPriceId = subPlan.MonthlyPriceId;
        item.Commission = subPlan.Commission;
        item.Level = subPlan.Level;
        item.PaymentMethod = subPlan.PaymentMethod;
        item.Tag = subPlan.Tag;
        item.Title = subPlan.Title;

        _db.SubscriptionPlans.Update(item);
        _db.SaveChanges();
    }

    public async Task Edit(SubscriptionPlanFeatureEdit feature)
    {
        var item = await _db.SubscriptionFeatures.Where(e => e.Id == feature.Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Subscription feature not found");

        item.BoolValue = feature.BoolValue;
        item.Name = feature.Name;
        item.Value = feature.Value;

        _db.SubscriptionFeatures.Update(item);
        _db.SaveChanges();
    }

    public async Task<SubscriptionPlanRead> Get(Guid Id)
    {
        var item = await _db.SubscriptionPlans.Include(e => e.Features).Where(e => e.Id == Id).SingleOrDefaultAsync();
        if (item == null) throw new Exception("Subscription plan not found");

        SubscriptionPlanRead result = new()
        {
            Annually = item.Annually,
            Discount = item.Discount,
            Id = item.Id,
            Monthly = item.Monthly,
            AnnualyPriceId = item.AnnualyPriceId,
            MonthlyPriceId = item.MonthlyPriceId,
            Commission = item.Commission,
            Order = item.Order,
            PaymentMethod = item.PaymentMethod,
            Tag = item.Tag,
            Level = item.Level,
            Title = item.Title,
            Features = item.Features.Select(e => new SubscriptionPlanFeatureRead
            {
                BoolValue = e.BoolValue,
                Id = e.Id,
                Name = e.Name,
                Value = e.Value
            }).ToList()
        };
        return result;
    }

    public async Task<PaginationList<SubscriptionPlanRead>> GetAll(int? page, int? cant)
    {
        PaginationList<SubscriptionPlanRead> list = new PaginationList<SubscriptionPlanRead>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var query = _db.SubscriptionPlans
        .Include(e => e.Features);

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.Order)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(item => new SubscriptionPlanRead
        {
            Annually = item.Annually,
            Discount = item.Discount,
            Id = item.Id,
            AnnualyPriceId = item.AnnualyPriceId,
            MonthlyPriceId = item.MonthlyPriceId,
            Commission = item.Commission,
            Monthly = item.Monthly,
            Order = item.Order,
            PaymentMethod = item.PaymentMethod,
            Tag = item.Tag,
            Title = item.Title,
            Features = item.Features.Select(e => new SubscriptionPlanFeatureRead
            {
                BoolValue = e.BoolValue,
                Id = e.Id,
                Name = e.Name,
                Value = e.Value
            }).ToList()
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }
}