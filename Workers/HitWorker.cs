using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.Models;

namespace WePromoLink.Workers;

public class HitWorker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 20;
    private readonly HitQueue _queue;
    private readonly DataContext _db;
    private readonly ILogger<HitWorker> _logger;

    public HitWorker(HitQueue queue, IServiceScopeFactory fac, ILogger<HitWorker> logger)
    {
        using (var scope = fac.CreateScope())
        {
            var s = scope.ServiceProvider.GetRequiredService<DataContext>();
            _db = s;
        }
        _queue = queue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            var item = _queue.Item;
            if (item != null)
            {
                await ProcessHit(item);
            }
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS));
        }
    }

    private async Task ProcessHit(HitAffiliate item)
    {
        try
        {
            using var dbtrans = _db.Database.BeginTransaction();
            var origin = item.Origin?.ToString();
            int afflinkId = await _db.AffiliateLinks.Where(e => e.ExternalId == item.AffLinkId).Select(e => e.Id).SingleOrDefaultAsync();
            var hit = await _db.HitAffiliates.Where(e => e.AffiliateLinkModelId == afflinkId && e.Origin == origin).SingleOrDefaultAsync();

            if (hit != null)
            {
                hit.Counter++;
                hit.LastHitAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
            }
            else
            {
                HitAffiliateModel model = new HitAffiliateModel
                {
                    AffiliateLinkModelId = afflinkId,
                    Counter = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsGeolocated = false,
                    LastHitAt = DateTime.UtcNow,
                    FirstHitAt = DateTime.UtcNow,
                    Origin = origin
                };
                _db.HitAffiliates.Add(model);
                await ProcessPaymentTransaction(model);
                await _db.SaveChangesAsync();
            }
            dbtrans.Commit();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private async Task ProcessPaymentTransaction(HitAffiliateModel model)
    {
        var affiliate = await _db.AffiliateLinks.Where(e => e.Id == model.AffiliateLinkModelId)
        .Include(e => e.SponsoredLink)
        .SingleOrDefaultAsync();

        if (affiliate == null) throw new Exception("Affiliate link not found");

        decimal amount = affiliate.SponsoredLink.EPM / 1000;
        var sponsored = affiliate.SponsoredLink;
        
        if(sponsored.Budget == 0) return;

        if (sponsored.Budget >= amount)
        {
            sponsored.Budget -= amount;
            affiliate.Available+=amount;
            affiliate.TotalEarned+=amount;
        } 
        else
        if(sponsored.Budget > 0)
        {
            amount = sponsored.Budget;
            sponsored.Budget = 0;
            affiliate.Available+=amount;
            affiliate.TotalEarned+=amount;
        }

        var transaction = new PaymentTransaction
        {
            AffiliateLinkId = affiliate.Id,
            SponsoredLinkId = sponsored.Id,
            Amount = amount,
            CompletedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            IsDeposit = false,
            Status = "COMPLETED",
            Title = "HIT",
            EmailModelId = affiliate.EmailModelId
        };

        _db.AffiliateLinks.Update(affiliate);
        _db.SponsoredLinks.Update(sponsored);
        await _db.PaymentTransactions.AddAsync(transaction);
        await _db.SaveChangesAsync().ConfigureAwait(false);
    }
}