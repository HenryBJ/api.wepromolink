using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface IAffiliateLinkService
{
    Task<object> CreateAffiliateLink(CreateAffiliateLink affLink, HttpContext ctx);
    Task<string> HitAffiliateLink(HitAffiliate hit);
    Task<AffLinkList> ListAffiliateLinks(int? page);
}