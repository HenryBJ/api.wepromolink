using Microsoft.EntityFrameworkCore;
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

    public async Task<SponsoredLinkList> ListSponsoredLinks(int? page)
    {
        SponsoredLinkList list = new SponsoredLinkList();
        int cant = 50;
        page = page ?? 1;
        page = page <= 0? 1: page;
        
        list.SponsoredLinks = await _db.SponsoredLinks
        .OrderByDescending(e=>e.CreatedAt)
        .Skip((page.Value!-1) * cant)
        .Take(cant)
        .Select(e=>new SponsoredLink
        {
            Budget = e.Budget,
            EPM = e.EPM,
            Id = e.ExternalId,
            ImageUrl = e.ImageUrl,
            Title = e.Title,
            Url = e.Url
        })
        .ToListAsync();
        list.Page = page.Value!;
        list.TotalPages = (await _db.SponsoredLinks.CountAsync()) / cant;
        return list;
    }
}