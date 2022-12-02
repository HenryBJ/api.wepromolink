using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WePromoLink.Data;
using WePromoLink.DTO;

namespace WePromoLink.Services;

public class StatsLinkService : IStatsLinkService
{
    private const int CACHE_EXPIRATION_MIN = 10;
    private readonly DataContext _db;
    private readonly IMemoryCache _cache;
    

    public StatsLinkService(DataContext db, IMemoryCache memoryCache)
    {
        _db = db;
        _cache = memoryCache;
    }

    public async Task<AffiliateLinkStats> AffiliateLinkStats(string affId)
    {
        if (!_cache.TryGetValue<AffiliateLinkStats>(affId, out AffiliateLinkStats result))
        {
            var affLink = await _db.AffiliateLinks.Where(e => e.ExternalId == affId)
            .Include(e => e.SponsoredLink)
            .SingleOrDefaultAsync();

            if (affLink == null) throw new Exception("Affiliate link not found");

            result = new AffiliateLinkStats
            {
                AffLinkId = affId,
                Available = affLink.Available,
                LastUpdated = affLink.LastUpdated,
                LastClick = affLink.LastClick,
                SponsoredLinkId = affLink.SponsoredLink.ExternalId,
                TotalEarned = affLink.TotalEarned,
                TotalWithdraw = affLink.TotalWithdraw,
                ValidClicks = await _db.HitAffiliates.Where(e => e.AffiliateLinkModelId == affLink.Id).CountAsync(),
                TotalClicks = await _db.HitAffiliates.Where(e => e.AffiliateLinkModelId == affLink.Id).Select(e => e.Counter).SumAsync()
            };
            _cache.Set<AffiliateLinkStats>(affId, result, TimeSpan.FromMinutes(CACHE_EXPIRATION_MIN));
        }

        return result;
    }

    public async Task<SponsoredLinkStats> SponsoredLinkStats(string sponsoredId)
    {

        if (!_cache.TryGetValue<SponsoredLinkStats>(sponsoredId, out SponsoredLinkStats result))
        {
            var sponsored = await _db.SponsoredLinks.Where(e => e.ExternalId == sponsoredId).SingleOrDefaultAsync();
            if (sponsored == null) throw new Exception("Sponsored link not found");

            result = new SponsoredLinkStats
            {
                RemainBudget = sponsored.Budget,
                LinkId = sponsoredId,
                LastClick = await _db.AffiliateLinks.Where(e => e.SponsoredLinkModelId == sponsored.Id).Select(e => e.LastClick).MaxAsync(),
                LastShared = await _db.AffiliateLinks.Where(e => e.SponsoredLinkModelId == sponsored.Id).Select(e => e.CreatedAt).MaxAsync(),
                LastUpdated = await _db.AffiliateLinks.Where(e => e.SponsoredLinkModelId == sponsored.Id).Select(e => e.LastUpdated).MaxAsync(),
                Shared = await _db.AffiliateLinks.Where(e => e.SponsoredLinkModelId == sponsored.Id).CountAsync(),
                ValidClicks = await _db.HitAffiliates.Where(e => e.AffiliateLink.SponsoredLinkModelId == sponsored.Id).CountAsync(),
                TotalClicks = await _db.HitAffiliates.Where(e => e.AffiliateLink.SponsoredLinkModelId == sponsored.Id).Select(e => e.Counter).SumAsync(),
                Spend = await _db.PaymentTransactions.Where(e => e.Title == "HIT" && e.SponsoredLinkId == sponsored.Id).Select(e => e.Amount).SumAsync()
            };
            _cache.Set<SponsoredLinkStats>(sponsoredId, result, TimeSpan.FromMinutes(CACHE_EXPIRATION_MIN));
        }
        return result;
    }
}