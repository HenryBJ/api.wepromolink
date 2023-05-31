using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ILinkService
{

    Task<string> Create(string ExternalCampaignId);

    // Task<object> CreateAffiliateLink(CreateAffiliateLink affLink, HttpContext ctx);
    Task<string> HitAffiliateLink(HitAffiliate hit);
    Task<AffLinkList> ListAffiliateLinks(int? page);
}