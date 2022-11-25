namespace WePromoLink.Services;

public class SponsoredLinkService : ISponsoredLinkService
{
    public Task<string> CreateSponsoredLink(CreateSponsoredLink link)
    {
        throw new NotImplementedException();
    }

    public Task FundSponsoredLink(string sponsoredLinkid)
    {
        throw new NotImplementedException();
    }

    public Task<SponsoredLinkList> ListSponsoredLinks(int? page)
    {
        throw new NotImplementedException();
    }
}