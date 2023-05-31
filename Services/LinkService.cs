using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Repositories;

namespace WePromoLink.Services;

public class LinkService : ILinkService
{
    private readonly DataContext _db;
    private readonly HitQueue _queue;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LinkService> _logger;

    public LinkService(DataContext ctx, HitQueue queue, IHttpContextAccessor httpContextAccessor, ILogger<LinkService> logger)
    {
        _db = ctx;
        _queue = queue;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<string> Create(string ExternalCampaignId)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);

        var userA = await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .SingleOrDefaultAsync();

        if (userA == null) throw new Exception("User no found");

        var campaign = await _db.Campaigns
        .Include(e => e.HistorySharedByUsersOnCampaign)
        .Include(e => e.HistorySharedOnCampaign)
        .Include(e => e.SharedLastWeekOnCampaign)
        .Include(e => e.SharedTodayOnCampaignModel)
        .SingleOrDefaultAsync();
        if (campaign == null) throw new Exception("Campaign no found");

        var userB = await _db.Users
        .Include(e => e.HistorySharedByUsersUser)
        .Include(e => e.SharedLastWeek)
        .Include(e => e.SharedToday)
        .Where(e => e.Id == campaign.UserModelId)
        .SingleOrDefaultAsync();
        if (userB == null) throw new Exception("User no found");

        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                var repo = new DataRepository(_db);
                string externalLinkId = await Nanoid.Nanoid.GenerateAsync(size: 16);

                // Create the link
                LinkModel link = new LinkModel
                {
                    Id = Guid.NewGuid(),
                    Url = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext!.Request.Host}/{externalLinkId}",
                    CampaignModelId = campaign.Id,
                    ExternalId = externalLinkId,
                    ClicksLastWeekOnLink = new ClicksLastWeekOnLinkModel(),
                    ClicksTodayOnLink = new ClicksTodayOnLinkModel(),
                    EarnLastWeekOnLink = new EarnLastWeekOnLinkModel(),
                    EarnTodayOnLink = new EarnTodayOnLinkModel(),
                    HistoryClicksByCountriesOnLink = new HistoryClicksByCountriesOnLinkModel(),
                    HistoryClicksOnLink = new HistoryClicksOnLinkModel(),
                    HistoryEarnByCountriesOnLink = new HistoryEarnByCountriesOnLinkModel(),
                    HistoryEarnOnLink = new HistoryEarnOnLinkModel(),
                    CreatedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    UserModelId = userA.Id
                };
                await _db.Links.AddAsync(link);
                await _db.SaveChangesAsync();

                // Update Campaign Statistics
                var historySharedByUsersOnCampaign = campaign.HistorySharedByUsersOnCampaign;
                await repo.Update(historySharedByUsersOnCampaign);

                var historySharedOnCampaign = campaign.HistorySharedOnCampaign;
                await repo.Update(historySharedOnCampaign);

                var sharedLastWeekOnCampaign = campaign.SharedLastWeekOnCampaign;
                await repo.Update(sharedLastWeekOnCampaign);

                var sharedTodayOnCampaignModel = campaign.SharedTodayOnCampaignModel;
                await repo.Update(sharedTodayOnCampaignModel);

                // Update User (campaign owner) Statistics
                var historySharedByUsersUser = userB.HistorySharedByUsersUser;
                await repo.Update(historySharedByUsersUser);

                var sharedLastWeek = userB.SharedLastWeek;
                await repo.Update(sharedLastWeek);

                var sharedToday = userB.SharedToday;
                await repo.Update(sharedToday);

                //Create a Notification
                var noti = new NotificationModel
                {
                    Id = Guid.NewGuid(),
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = NotificationStatusEnum.Unread,
                    UserModelId = userB.Id,
                    Title = "Campaign shared",
                    Message = $"A link has been created to your campaign called '{campaign.Title}' by the user {userA.Fullname}",
                };                
                await _db.Notifications.AddAsync(noti);
                await _db.SaveChangesAsync();

                transaction.Commit();
                return link.Url;
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex.Message);
                throw new Exception("Error creating link (share)");
            }
        }
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