using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public class AffiliateLinkService : IAffiliateLinkService
{
    private readonly DataContext _db;
     private readonly HitQueue _queue;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AffiliateLinkService(DataContext ctx, HitQueue queue, IHttpContextAccessor httpContextAccessor)
    {
        _db = ctx;
        _queue = queue;
        _httpContextAccessor = httpContextAccessor;
    }

    // public async Task<object> CreateAffiliateLink(CreateAffiliateLink affLink, HttpContext ctx)
    // {
    //     var sponsoredLink = await _db.Campaigns.Where(e=>e.ExternalId == affLink.SponsoredLinkId).SingleOrDefaultAsync(); 
    //     if(sponsoredLink == null) throw new Exception("Sponsored link not found");

    //     var email = await _db.Emails.Where(e=>e.Email.ToLower() == affLink.Email!.ToLower()).SingleOrDefaultAsync();
    //     if(email == null)
    //     {
    //         email = new EmailModel
    //         {
    //             CreatedAt = DateTime.UtcNow,
    //             Email = affLink.Email!
    //         };
    //         _db.Emails.Add(email);
    //         await _db.SaveChangesAsync();
    //     }
        
    //     var externalId = await Nanoid.Nanoid.GenerateAsync(size:16);
    //     AffiliateLinkModel affiliateLinkModel = new AffiliateLinkModel
    //     {
    //         Available = 0m,
    //         BTCAddress = affLink.BTCAddress,
    //         CreatedAt = DateTime.UtcNow,
    //         EmailModelId = email.Id,
    //         ExternalId = externalId,
    //         Group = affLink.Options?.Group,
    //         CampaignModelId = sponsoredLink.Id,
    //         Threshold = affLink.Options?.Threshold??0m
    //     };
    //     _db.AffiliateLinks.Add(affiliateLinkModel);
    //     await _db.SaveChangesAsync();
    //     return new {id=externalId, link=$"{ctx.Request.Scheme}://{ctx.Request.Host}/{externalId}"};
    // }

    public async Task<string> HitAffiliateLink(HitAffiliate hit)
    {
    //    var affiliateLink = await _db.AffiliateLinks.Where(e=>e.ExternalId == hit.AffLinkId)
    //    .Include(e=>e.Campaign)
    //    .SingleOrDefaultAsync();

    //    if(affiliateLink == null) throw new Exception($"Affiliate link not found: {hit.AffLinkId}");
    //    _queue.Item = hit; // Add for forward processing 
    //    return affiliateLink.Campaign.Url;
    return "";
    }

    public async Task<AffLinkList> ListAffiliateLinks(int? page)
    {
        return null;
        // AffLinkList list = new AffLinkList();
        // int cant = 50;
        // page = page ?? 1;
        // page = page <= 0 ? 1 : page;

        // list.AffLinks = await _db.AffiliateLinks
        // .Include(e=>e.Campaign)
        // .OrderByDescending(e => e.CreatedAt)
        // .Skip((page.Value! - 1) * cant)
        // .Take(cant)
        // .Select(e => new AffLink
        // {
        //     Available = e.Available,
        //     Id = e.ExternalId,
        //     ImageUrl = e.Campaign.ImageUrl,
        //     Title = $"affiliate: {e.Campaign.Title}",
        //     Url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/{e.ExternalId}"
        // })
        // .ToListAsync();
        // list.Page = page.Value!;
        // list.TotalPages = ((await _db.Campaigns.CountAsync()) / cant) + 1;
        // return list;
    }
}