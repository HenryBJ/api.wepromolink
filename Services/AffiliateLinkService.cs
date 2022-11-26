using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.Models;

namespace WePromoLink.Services;

public class AffiliateLinkService : IAffiliateLinkService
{
    private readonly DataContext _db;

    public AffiliateLinkService(DataContext ctx)
    {
        _db = ctx;
    }

    public async Task<string> CreateAffiliateLink(CreateAffiliateLink affLink)
    {
        var sponsoredLink = await _db.SponsoredLinks.Where(e=>e.ExternalId == affLink.SponsoredLinkId).SingleOrDefaultAsync(); 
        if(sponsoredLink == null) throw new Exception("Sponsored link not found");

        var email = await _db.Emails.Where(e=>e.Email.ToLower() == affLink.Email.ToLower()).SingleOrDefaultAsync();
        if(email == null)
        {
            email = new EmailModel
            {
                CreatedAt = DateTime.UtcNow,
                Email = affLink.Email!
            };
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();
        }
        
        var externalId = await Nanoid.Nanoid.GenerateAsync(size:16);
        AffiliateLinkModel affiliateLinkModel = new AffiliateLinkModel
        {
            Available = 0m,
            BTCAddress = affLink.BTCAddress,
            CreatedAt = DateTime.UtcNow,
            EmailModelId = email.Id,
            ExternalId = externalId,
            Group = affLink.Options?.Group,
            SponsoredLinkModelId = sponsoredLink.Id,
            Threshold = affLink.Options?.Threshold??0m
        };
        _db.AffiliateLinks.Add(affiliateLinkModel);
        await _db.SaveChangesAsync();
        return externalId;
    }

    public Task<string> HitAffiliateLink(HitAffiliate hit)
    {
        throw new NotImplementedException();
    }
}