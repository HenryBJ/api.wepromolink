using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface ISponsoredLinkService
{
    Task<string> CreateSponsoredLink(CreateSponsoredLink link);
    Task<SponsoredLinkList> ListSponsoredLinks(int? page);
    Task<string> FundSponsoredLink(FundSponsoredLink fundLink);
}