namespace WePromoLink.Services;

public interface IAffiliateLinkService
{
    Task<string> CreateAffiliateLink(CreateAffiliateLink affLink);
    Task<string> HitAffiliateLink(HitAffiliate hit);
}