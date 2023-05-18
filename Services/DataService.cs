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
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DataService(DataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    private IResult AddCacheHistoricalData<T, R, L>(T result) where T : HistoryStatsBaseModel<R,L>
    {
        if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("If-None-Match", out StringValues requestETagValues))
        {
            if (requestETagValues[0] == result.Etag)
            {
                _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
                _httpContextAccessor.HttpContext.Response.StatusCode = 304;
                return null;
            }
        }

        _httpContextAccessor.HttpContext!.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
        {
            Public = true,
            MaxAge = result.MaxAge!.Value
        };
        _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
        return Results.Ok(ConvertToHistoricalData<T, R, L>(result));
    }

    private HistoricalData<R> ConvertToHistoricalData<T, R, L>(T result) where T : HistoryStatsBaseModel<R,L>
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

    private IResult AddCache<T, R>(T result) where T : StatsBaseModel, IHasValue<R>
    {
        if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue("If-None-Match", out StringValues requestETagValues))
        {
            if (requestETagValues[0] == result.Etag)
            {
                _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
                _httpContextAccessor.HttpContext.Response.StatusCode = 304;
                return null;
            }
        }

        _httpContextAccessor.HttpContext!.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
        {
            Public = true,
            MaxAge = result.MaxAge!.Value
        };
        _httpContextAccessor.HttpContext.Response.Headers.ETag = result.Etag;
        return Results.Ok(result.Value);
    }

    public async Task<IResult> GetAvailable()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();
        if (user != null) return AddCache<AvailableModel, decimal>(user.Available);
        return Results.NotFound();
    }

    public async Task<IResult> GetBudget()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Budget).SingleOrDefaultAsync();
        if (user != null) return AddCache<BudgetModel, decimal>(user.Budget);
        return Results.NotFound();
    }

    public async Task<IResult> GetLocked()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Locked).SingleOrDefaultAsync();
        if (user != null) return AddCache<LockedModel, decimal>(user.Locked);
        return Results.NotFound();
    }

    public async Task<IResult> GetPayoutStats()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.PayoutStat).SingleOrDefaultAsync();
        if (user != null) return AddCache<PayoutStatModel, decimal>(user.PayoutStat);
        return Results.NotFound();
    }

    public async Task<IResult> GetProfit()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Profit).SingleOrDefaultAsync();
        if (user != null) return AddCache<ProfitModel, decimal>(user.Profit);
        return Results.NotFound();
    }

    public async Task<IResult> GetEarnToday()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.EarnTodayUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<EarnTodayUserModel, decimal>(user.EarnTodayUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetEarnLastWeek()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.EarnLastWeekUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<EarnLastWeekUserModel, decimal>(user.EarnLastWeekUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetClickTodayOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClicksTodayOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClicksTodayOnLinksUserModel, int>(user.ClicksTodayOnLinksUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetClickLastWeekOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClickLastWeekOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClickLastWeekOnLinksUserModel, int>(user.ClickLastWeekOnLinksUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetClickTodayOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClicksTodayOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClicksTodayOnCampaignUserModel, int>(user.ClicksTodayOnCampaignUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetClickLastWeekOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.ClicksLastWeekOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCache<ClicksLastWeekOnCampaignUserModel, int>(user.ClicksLastWeekOnCampaignUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetSharedToday()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.SharedToday).SingleOrDefaultAsync();
        if (user != null) return AddCache<SharedTodayUserModel, int>(user.SharedToday);
        return Results.NotFound();
    }

    public async Task<IResult> GetSharedLastWeek()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.SharedLastWeek).SingleOrDefaultAsync();
        if (user != null) return AddCache<SharedLastWeekUserModel, int>(user.SharedLastWeek);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalClickOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksOnLinksUserModel, int, DateTime>(user.HistoryClicksOnLinksUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalEarnOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryEarnOnLinksUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryEarnOnLinksUserModel, decimal, DateTime>(user.HistoryEarnOnLinksUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalClickOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksOnCampaignUserModel, int, DateTime>(user.HistoryClicksOnCampaignUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalClickOnShares()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksOnSharesUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksOnSharesUserModel, int, DateTime>(user.HistoryClicksOnSharesUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalClickByCountriesOnLinks()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksByCountriesOnLinkUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksByCountriesOnLinkUserModel, int, string>(user.HistoryClicksByCountriesOnLinkUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalEarnByCountries()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryEarnByCountriesUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryEarnByCountriesUserModel, decimal, string>(user.HistoryEarnByCountriesUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalClickByCountriesOnCampaigns()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistoryClicksByCountriesOnCampaignUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistoryClicksByCountriesOnCampaignUserModel, int, string>(user.HistoryClicksByCountriesOnCampaignUser);
        return Results.NotFound();
    }

    public async Task<IResult> GetHistoricalSharedByUsers()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.HistorySharedByUsersUser).SingleOrDefaultAsync();
        if (user != null) return AddCacheHistoricalData<HistorySharedByUsersUserModel, int, string>(user.HistorySharedByUsersUser);
        return Results.NotFound();
    }
}