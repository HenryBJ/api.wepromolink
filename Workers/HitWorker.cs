using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;

namespace WePromoLink.Workers;

public class HitWorker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 60;
    private readonly HitQueue _queue;
    private readonly DataContext _db;
    private readonly ILogger<HitWorker> _logger;

    public HitWorker(HitQueue queue, DataContext db, ILogger<HitWorker> logger)
    {
        _queue = queue;
        _db = db;
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
                await _db.SaveChangesAsync();
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}