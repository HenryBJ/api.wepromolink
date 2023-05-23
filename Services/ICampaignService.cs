using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ICampaignService
{
    Task<string> CreateCampaign(Campaign campaign);
    Task<MyCampaignList> GetAll(int? page, int? cant, string? filter);
    Task<CampaignDetail> GetDetails(string id);
    Task Edit(string id, Campaign campaign);
    Task Delete(string id);
    Task Publish(string campaignId, bool toStatus);
    Task<IActionResult> Explore(int page);


    // Task<string> FundSponsoredLink(FundSponsoredLink fundLink);
}