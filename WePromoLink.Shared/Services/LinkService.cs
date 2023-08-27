using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Repositories;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Services;

public class LinkService : ILinkService
{
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LinkService> _logger;
    private readonly MessageBroker<Hit> _messageBroker;
    private readonly MessageBroker<UpdateUserMessage> _userMessageBroker;
    private readonly MessageBroker<UpdateCampaignMessage> _campaignMessageBroker;
    private readonly MessageBroker<BaseEvent> _eventSender;

    public LinkService(
        DataContext ctx,
        IHttpContextAccessor httpContextAccessor,
        ILogger<LinkService> logger,
        MessageBroker<UpdateUserMessage> userMessageBroker,
        MessageBroker<UpdateCampaignMessage> campaignMessageBroker,
        MessageBroker<BaseEvent> eventSender,
        MessageBroker<Hit> messageBroker)
    {
        _db = ctx;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _userMessageBroker = userMessageBroker;
        _campaignMessageBroker = campaignMessageBroker;
        _eventSender = eventSender;
        _messageBroker = messageBroker;
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
        .Where(e => e.ExternalId == ExternalCampaignId)
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
                string externalLinkId = await Nanoid.Nanoid.GenerateAsync(size: 16);

                // Create the link
                LinkModel link = new LinkModel
                {
                    Id = Guid.NewGuid(),
                    Url = $"https://wepromolink.io/{externalLinkId}",
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
                _campaignMessageBroker.Send(new UpdateCampaignMessage { Id = campaign.Id });

                // Update User (campaign owner) Statistics
                _userMessageBroker.Send(new UpdateUserMessage { Id = userB.Id });

                transaction.Commit();

                _eventSender.Send(new CampaignSharedEvent
                {
                    Amount = campaign.Budget,
                    CampaignId = campaign.Id,
                    CampaignName = campaign.Title,
                    OwnerName = userB.Fullname,
                    OwnerUserId = userB.Id,
                    EPM = campaign.EPM,
                    SharedByName = userA.Fullname,
                    SharedByUserId = userA.Id
                });

                _eventSender.Send(new LinkCreatedEvent
                {
                    CampaignId = campaign.Id,
                    CampaignName = campaign.Title,
                    LinkCreatorName = userA.Fullname,
                    LinkCreatorUserId = userA.Id,
                    LinkId = link.Id,
                    OwnerName = userB.Fullname,
                    OwnerUserId = userB.Id
                });

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
        var Link = await _db.Links.Where(e => e.ExternalId == hit.LinkId)
        .Include(e => e.Campaign)
        .SingleOrDefaultAsync();

        if (Link == null) return string.Empty;
        _messageBroker.Send(hit); // Send to be process in Hit Workers 
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

        var query = _db.Links
        .Include(e => e.Campaign)
        .ThenInclude(e => e.ImageData)
        .Where(e => e.UserModelId == userId);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.Campaign.Title.ToLower().Contains(filter.ToLower()));
        }

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new MyLink
        {
            Id = e.ExternalId,
            ImageData = e.Campaign.ImageData != null ? new ImageData
            {
                ExternalId = e.Campaign.ImageData.ExternalId,
                Compressed = e.Campaign.ImageData.Compressed,
                CompressedAspectRatio = e.Campaign.ImageData.CompressedAspectRatio,
                CompressedHeight = e.Campaign.ImageData.CompressedHeight,
                CompressedWidth = e.Campaign.ImageData.CompressedWidth,
                Medium = e.Campaign.ImageData.Medium,
                MediumAspectRatio = e.Campaign.ImageData.MediumAspectRatio,
                MediumHeight = e.Campaign.ImageData.MediumHeight,
                MediumWidth = e.Campaign.ImageData.MediumWidth,
                Original = e.Campaign.ImageData.Original,
                OriginalAspectRatio = e.Campaign.ImageData.OriginalAspectRatio,
                OriginalHeight = e.Campaign.ImageData.OriginalHeight,
                OriginalWidth = e.Campaign.ImageData.OriginalWidth,
                Thumbnail = e.Campaign.ImageData.Thumbnail,
                ThumbnailAspectRatio = e.Campaign.ImageData.ThumbnailAspectRatio,
                ThumbnailHeight = e.Campaign.ImageData.ThumbnailHeight,
                ThumbnailWidth = e.Campaign.ImageData.ThumbnailWidth
            } : null,
            Title = e.Campaign.Title,
            Url = e.Url,
            Status = e.Campaign.Status,
            LastClick = e.LastClick,
            Profit = _db.PaymentTransactions.Where(k => k.LinkModelId == e.Id).Select(e => e.Amount).Sum()
        })
        .ToListAsync();

        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
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
        .Include(e => e.Campaign)
        .ThenInclude(e => e.ImageData)
        .Where(e => e.ExternalId == id).Select(e => new LinkDetail
        {
            Id = e.ExternalId,
            ImageData = e.Campaign.ImageData != null ? new ImageData
            {
                Compressed = e.Campaign.ImageData.Compressed,
                CompressedAspectRatio = e.Campaign.ImageData.CompressedAspectRatio,
                CompressedHeight = e.Campaign.ImageData.CompressedHeight,
                CompressedWidth = e.Campaign.ImageData.CompressedWidth,
                ExternalId = e.Campaign.ImageData.ExternalId,
                Medium = e.Campaign.ImageData.Medium,
                MediumAspectRatio = e.Campaign.ImageData.MediumAspectRatio,
                MediumHeight = e.Campaign.ImageData.MediumHeight,
                MediumWidth = e.Campaign.ImageData.MediumWidth,
                Original = e.Campaign.ImageData.Original,
                OriginalAspectRatio = e.Campaign.ImageData.OriginalAspectRatio,
                OriginalHeight = e.Campaign.ImageData.OriginalHeight,
                OriginalWidth = e.Campaign.ImageData.OriginalWidth,
                Thumbnail = e.Campaign.ImageData.Thumbnail,
                ThumbnailAspectRatio = e.Campaign.ImageData.ThumbnailAspectRatio,
                ThumbnailHeight = e.Campaign.ImageData.ThumbnailHeight,
                ThumbnailWidth = e.Campaign.ImageData.ThumbnailWidth
            } : null,
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