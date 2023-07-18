using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ILinkService
{

    Task<string> Create(string ExternalCampaignId);
    Task<PaginationList<MyLink>> GetAll(int? page, int? cant, string? filter);
    Task<LinkDetail> GetDetails(string id);

    // Task<object> CreateAffiliateLink(CreateAffiliateLink affLink, HttpContext ctx);
    Task<string> HitLink(Hit hit);
}