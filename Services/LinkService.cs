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

    public async Task<string> HitLink(Hit hit)
    {
           var Link = await _db.Links.Where(e=>e.ExternalId == hit.LinkId)
           .Include(e=>e.Campaign)
           .SingleOrDefaultAsync();

           if(Link == null) return string.Empty;
           _queue.Item = hit; // Add for forward processing 
           return Link.Campaign.Url;
    }

    public async Task<PaginationList<MyLink>> GetAll(int? page, int? cant, string? filter)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        PaginationList<MyLink> list = new PaginationList<MyLink>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 11;

        var counter = await _db.Links
        .Include(e => e.Campaign)
        .Where(e => e.UserModelId == userId)
        .CountAsync();

        var query = _db.Links
        .Include(e => e.Campaign)
        .Where(e => e.UserModelId == userId);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.Campaign.Title.ToLower().Contains(filter.ToLower()));
        }

        list.Items = await query
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new MyLink
        {
            Id = e.ExternalId,
            ImageUrl = e.Campaign.ImageUrl,
            Title = e.Campaign.Title,
            Url = e.Url,
            Status = e.Campaign.Status,
            LastClick = e.LastClick,
            Profit = _db.PaymentTransactions.Where(k => k.LinkModelId == e.Id).Select(e => e.Amount).Sum()
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (counter / cant!.Value) + 1;
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    public async Task<LinkDetail> GetDetails(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links
        .Include(e=>e.Campaign)
        .Where(e => e.ExternalId == id).Select(e => new LinkDetail
        {
            Id = e.ExternalId,
            ImageUrl = e.Campaign.ImageUrl!,
            Status = e.Campaign.Status,
            Title = e.Campaign.Title,
            Url = e.Url,
            LastClick = e.LastClick,
            Description = e.Campaign.Description!,
            Epm = e.Campaign.EPM,
            Profit = _db.PaymentTransactions.Where(k => k.LinkModelId == e.Id).Select(e => e.Amount).Sum()  
        }).SingleOrDefaultAsync();

        if (link == null) throw new Exception("Link does not exits");
        return link;
    }
}