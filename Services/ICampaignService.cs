using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ICampaignService
{
    Task<string> CreateCampaign(Campaign campaign);
    Task<PaginationList<MyCampaign>> GetAll(int? page, int? cant, string? filter);
    Task<CampaignDetail> GetDetails(string id);
    Task Edit(string id, Campaign campaign);
    Task Delete(string id);
    Task Publish(string campaignId, bool toStatus);
    Task<IActionResult> Explore(int offset, int limit, long timestamp);


    // Task<string> FundSponsoredLink(FundSponsoredLink fundLink);
}