using WePromoLink.Data;
using WePromoLink.Models;

namespace WePromoLink.Services;

public class SponsoredLinkService : ISponsoredLinkService
{
    private readonly DataContext _db;

    public SponsoredLinkService(DataContext db)
    {
        _db = db;
    }
    public async Task<string> CreateSponsoredLink(CreateSponsoredLink link)
    {
        var email = _db.Emails.Where(e=>e.Email.ToLower() == link.Email!.ToLower()).SingleOrDefault();
        if(email == null)
        {
            email = new EmailModel{CreatedAt = DateTime.UtcNow, Email = link.Email!.ToLower()};
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }

        string  externalId = await Nanoid.Nanoid.GenerateAsync(size:12);

         SponsoredLinkModel slink = new SponsoredLinkModel
         {
            ExternalId = externalId,
            Budget = 0,
            CreatedAt = DateTime.UtcNow,
            EmailModelId = email.Id,
            EPM = link.EPM,
            ImageUrl = link.ImageUrl,
            Title = link.Title!,
            Description = link.Description,
            RemainBudget = 0,
            Url = link.Url!            
         }; 

         _db.SponsoredLinks.Add(slink);
         await _db.SaveChangesAsync();

         return externalId;
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