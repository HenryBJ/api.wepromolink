namespace WePromoLink.Services;

public class StatsLinkService : IStatsLinkService
{
    public Task<AffiliateLinkStats> AffiliateLinkStats(string affId)
    {
        throw new NotImplementedException();
    }

    public Task<SponsoredLinkStats> SponsoredLinkStats(string sponsoredId)
    {
        throw new NotImplementedException();
    }
}