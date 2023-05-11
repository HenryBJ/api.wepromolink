using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ICampaignService
{
    Task<string> CreateCampaign(Campaign campaign);
    Task<MyCampaignList> GetAll(int? page, int? cant, string? filter);
    // Task<string> FundSponsoredLink(FundSponsoredLink fundLink);
}