
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WePromoLink.Data;
using WePromoLink.DTO.SignalR;
using WePromoLink.Enums;
using WePromoLink.Services.Cache;
using WePromoLink.Services.SignalR;

namespace WePromoLink.Services.SignalR;

public class DashboardHub : Hub
{
    private const string HUB_NAME_KEY = "dashboard_admin_key";
    private readonly IServiceScopeFactory _fac;
    private readonly IShareCache _cache;
    public DashboardHub(IServiceScopeFactory fac, IShareCache cache)
    {
        _fac = fac;
        _cache = cache;
    }

    public async Task InitialLoad()
    {
        var dashboardStatus = _cache.Get<DashboardStatus>(HUB_NAME_KEY);
        if (dashboardStatus == null)
        {
            dashboardStatus = await CreateStatus();
            _cache.Set(HUB_NAME_KEY, dashboardStatus);
        }
        await Clients.Caller.SendAsync("initialLoadResponse", dashboardStatus);
    }

    private async Task<DashboardStatus> CreateStatus()
    {
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        return new DashboardStatus
        {
            Clicks = await _db.Hits.LongCountAsync(),
            CampaignBudget = await _db.Campaigns.SumAsync(e => e.Budget),
            Campaigns = await _db.Campaigns.LongCountAsync(),
            Deposit = await _db.PaymentTransactions.Where(e => e.TransactionType == TransactionTypeEnum.Deposit).LongCountAsync(),
            Withdraw = await _db.PaymentTransactions.Where(e => e.TransactionType == TransactionTypeEnum.Withdraw).LongCountAsync(),
            Hits = await _db.Hits.Select(e => e.Counter).SumAsync(),
            GeoLocations = await _db.GeoDatas.LongCountAsync(),
            RegisteredUsers = await _db.Users.LongCountAsync(),
            Shareds = await _db.Links.LongCountAsync(),
            TotalAvailable = await _db.Availables.Select(e => e.Value).SumAsync(),
            TotalProfit = await _db.Profits.Select(e => e.Value).SumAsync(),
            Transactions = await _db.PaymentTransactions.LongCountAsync(),
            UnVerifiedUsers = await _db.BitcoinBillings.Where(e => !e.IsVerified).LongCountAsync() + await _db.StripeBillings.Where(e => !e.IsVerified).LongCountAsync(),
            VerifiedUsers = await _db.BitcoinBillings.Where(e => e.IsVerified).LongCountAsync() + await _db.StripeBillings.Where(e => e.IsVerified).LongCountAsync(),
            LastModified = DateTime.UtcNow
        };
    }
}