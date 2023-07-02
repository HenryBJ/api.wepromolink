using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Interfaces;
using WePromoLink.Models;

namespace WePromoLink.Services;

public class DataService : IDataService
{
    private readonly DataContext _db;
    private readonly ILogger<DataService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DataService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<DataService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    private IActionResult AddCacheHistoricalData<T, R, L>(T result) where T : HistoryStatsBaseModel<R, L>
    {
        try
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("If-None-Match", out StringValues requestETagValues))
            {
                if (requestETagValues[0] == result.Etag)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
                    //_httpContextAccessor.HttpContext.Response.StatusCode = 304;
                    return new StatusCodeResult(304);
                }
            }

            _httpContextAccessor.HttpContext!.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                Public = true,
                MaxAge = result.MaxAge!.Value
            };
            _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
            return new OkObjectResult(ConvertToHistoricalData<T, R, L>(result));
        }
        catch (System.Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new OkObjectResult(ConvertToHistoricalData<T, R, L>(result));
        }
    }

    private HistoricalData<R> ConvertToHistoricalData<T, R, L>(T result) where T : HistoryStatsBaseModel<R, L>
    {
        var item = new HistoricalData<R>();
        item.DataLabels = new List<string>();
        item.DataLabels.Add("Data");
        item.Labels = new List<string>();
        item.Labels.Add(result?.L0?.ToString() ?? "");
        item.Labels.Add(result?.L1?.ToString() ?? "");
        item.Labels.Add(result?.L2?.ToString() ?? "");
        item.Labels.Add(result?.L3?.ToString() ?? "");
        item.Labels.Add(result?.L4?.ToString() ?? "");
        item.Labels.Add(result?.L5?.ToString() ?? "");
        item.Labels.Add(result?.L6?.ToString() ?? "");
        item.Labels.Add(result?.L7?.ToString() ?? "");
        item.Labels.Add(result?.L8?.ToString() ?? "");
        item.Labels.Add(result?.L9?.ToString() ?? "");
        item.Data = new List<List<R>>();
        item.Data.Add(new List<R>{
            result!.X0!,
            result.X1!,
            result.X2!,
            result.X3!,
            result.X4!,
            result.X5!,
            result.X6!,
            result.X7!,
            result.X8!,
            result.X9!
        });
        return item;
    }

    private IActionResult AddCache<T, R>(T result) where T : StatsBaseModel, IHasValue<R>
    {
        try
        {
            if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("If-None-Match", out StringValues requestETagValues))
            {
                if (requestETagValues[0] == result.Etag)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
                    // _httpContextAccessor.HttpContext.Response.StatusCode = 304;
                    return new StatusCodeResult(304);
                }
            }

            _httpContextAccessor.HttpContext!.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
            {
                Public = true,
                MaxAge = result.MaxAge!.Value
            };
            _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
            return new OkObjectResult(result.Value);
        }
        catch (System.Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new OkObjectResult(result.Value);
        }

    }

    public async Task<IActionResult> GetAvailable()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();
        if (user != null) return AddCache<AvailableModel, decimal>(user.Available);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetBudget()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Budget).SingleOrDefaultAsync();
        if (user != null) return AddCache<BudgetModel, decimal>(user.Budget);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetLocked()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Locked).SingleOrDefaultAsync();
        if (user != null) return AddCache<LockedModel, decimal>(user.Locked);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetPayoutStats()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.PayoutStat).SingleOrDefaultAsync();
        if (user != null) return AddCache<PayoutStatModel, decimal>(user.PayoutStat);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetProfit()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Profit).SingleOrDefaultAsync();
        if (user != null) return AddCache<ProfitModel, decimal>(user.Profit);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetEarnToday()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.EarnTodayUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<EarnTodayUserModel, decimal>(user.EarnTodayUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetEarnLastWeek()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.EarnLastWeekUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<EarnLastWeekUserModel, decimal>(user.EarnLastWeekUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetClickTodayOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClicksTodayOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClicksTodayOnLinksUserModel, int>(user.ClicksTodayOnLinksUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetClickLastWeekOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClickLastWeekOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClickLastWeekOnLinksUserModel, int>(user.ClickLastWeekOnLinksUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetClickTodayOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClicksTodayOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClicksTodayOnCampaignUserModel, int>(user.ClicksTodayOnCampaignUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetClickLastWeekOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClicksLastWeekOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClicksLastWeekOnCampaignUserModel, int>(user.ClicksLastWeekOnCampaignUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetSharedToday()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.SharedToday).SingleOrDefaultAsync();
        if (user != null) return AddCache<SharedTodayUserModel, int>(user.SharedToday);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetSharedLastWeek()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.SharedLastWeek).SingleOrDefaultAsync();
        if (user != null) return AddCache<SharedLastWeekUserModel, int>(user.SharedLastWeek);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalClickOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksOnLinksUserModel, int, DateTime>(user.HistoryClicksOnLinksUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalEarnOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryEarnOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryEarnOnLinksUserModel, decimal, DateTime>(user.HistoryEarnOnLinksUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalClickOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksOnCampaignUserModel, int, DateTime>(user.HistoryClicksOnCampaignUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalClickOnShares()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksOnSharesUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksOnSharesUserModel, int, DateTime>(user.HistoryClicksOnSharesUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalClickByCountriesOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksByCountriesOnLinkUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksByCountriesOnLinkUserModel, int, string>(user.HistoryClicksByCountriesOnLinkUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalEarnByCountries()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryEarnByCountriesUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryEarnByCountriesUserModel, decimal, string>(user.HistoryEarnByCountriesUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalClickByCountriesOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksByCountriesOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksByCountriesOnCampaignUserModel, int, string>(user.HistoryClicksByCountriesOnCampaignUser);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetHistoricalSharedByUsers()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistorySharedByUsersUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistorySharedByUsersUserModel, int, string>(user.HistorySharedByUsersUser);
        return new NotFoundResult();
    }

    // Campaigns

    public async Task<IActionResult> GetClicksLastWeekOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.ClicksLastWeekOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCache<ClicksLastWeekOnCampaignModel, int>(campaign.ClicksLastWeekOnCampaign);
    }

    public async Task<IActionResult> GetClicksTodayOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.ClicksTodayOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCache<ClicksTodayOnCampaignModel, int>(campaign.ClicksTodayOnCampaign);
    }

    public async Task<IActionResult> GetHistoryClicksByCountriesOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.HistoryClicksByCountriesOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistoryClicksByCountriesOnCampaignModel, int, string>(campaign.HistoryClicksByCountriesOnCampaign);
    }

    public async Task<IActionResult> GetHistoryClicksOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.HistoryClicksOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistoryClicksOnCampaignModel, int, DateTime>(campaign.HistoryClicksOnCampaign);
    }

    public async Task<IActionResult> GetHistorySharedByUsersOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.HistorySharedByUsersOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistorySharedByUsersOnCampaignModel, int, string>(campaign.HistorySharedByUsersOnCampaign);
    }

    public async Task<IActionResult> GetHistorySharedOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.HistorySharedOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistorySharedOnCampaignModel, int, DateTime>(campaign.HistorySharedOnCampaign);
    }

    public async Task<IActionResult> GetSharedLastWeekOnCampaign(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.SharedLastWeekOnCampaign).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCache<SharedLastWeekOnCampaignModel, int>(campaign.SharedLastWeekOnCampaign);
    }

    public async Task<IActionResult> GetSharedTodayOnCampaignModel(string campaignId)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns.Where(e => e.UserModelId == userId && e.ExternalId == campaignId)
        .Include(e => e.SharedTodayOnCampaignModel).SingleOrDefaultAsync();

        if (campaign == null) return new NotFoundResult();
        return AddCache<SharedTodayOnCampaignModel, int>(campaign.SharedTodayOnCampaignModel);
    }

    // Links

    public async Task<IActionResult> GetClicksLastWeekOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.ClicksLastWeekOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCache<ClicksLastWeekOnLinkModel, int>(link.ClicksLastWeekOnLink);
    }

    public async Task<IActionResult> GetClicksTodayOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.ClicksTodayOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCache<ClicksTodayOnLinkModel, int>(link.ClicksTodayOnLink);
    }

    public async Task<IActionResult> GetEarnLastWeekOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.EarnLastWeekOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCache<EarnLastWeekOnLinkModel, decimal>(link.EarnLastWeekOnLink);
    }

    public async Task<IActionResult> GetEarnTodayOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.EarnTodayOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCache<EarnTodayOnLinkModel, decimal>(link.EarnTodayOnLink);
    }

    public async Task<IActionResult> GetHistoryClicksByCountriesOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.HistoryClicksByCountriesOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistoryClicksByCountriesOnLinkModel, int, string>(link.HistoryClicksByCountriesOnLink);
    }

    public async Task<IActionResult> GetHistoryEarnByCountriesOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.HistoryEarnByCountriesOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistoryEarnByCountriesOnLinkModel, decimal, string>(link.HistoryEarnByCountriesOnLink);
    }

    public async Task<IActionResult> GetHistoryEarnOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.HistoryEarnOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistoryEarnOnLinkModel, decimal, DateTime>(link.HistoryEarnOnLink);
    }

    public async Task<IActionResult> GetHistoryClicksOnLink(string linkId)
    {
        if (string.IsNullOrEmpty(linkId)) throw new Exception("Invalid Link ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var link = await _db.Links.Where(e => e.UserModelId == userId && e.ExternalId == linkId)
        .Include(e => e.HistoryClicksOnLink).SingleOrDefaultAsync();

        if (link == null) return new NotFoundResult();
        return AddCacheHistoricalData<HistoryClicksOnLinkModel, int, DateTime>(link.HistoryClicksOnLink);
    }
}