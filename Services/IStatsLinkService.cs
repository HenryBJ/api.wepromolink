using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface IStatsLinkService 
{
    Task<AffiliateLinkStats> AffiliateLinkStats(string affId);
    Task<SponsoredLinkStats> SponsoredLinkStats(string sponsoredId);
}