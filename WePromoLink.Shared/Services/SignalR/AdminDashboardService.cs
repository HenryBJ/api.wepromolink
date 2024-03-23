using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WePromoLink.Data;
using WePromoLink.DTO.SignalR;
using WePromoLink.Enums;
using WePromoLink.Services.Cache;

namespace WePromoLink.Services.SignalR;

public class AdminDashboardService: IAdminDashboardHub
{
    private const string HUB_NAME_KEY = "dashboard_admin_key";
    private readonly IShareCache _cache;
    private readonly IServiceScopeFactory _fac;
    private readonly IHubContext<DashboardHub> _hub;

    public AdminDashboardService(IShareCache cache, IServiceScopeFactory fac)
    {
        _cache = cache;
        _fac = fac;
        _hub = fac.CreateScope().ServiceProvider.GetRequiredService<IHubContext<DashboardHub>>();
    }


    public async Task UpdateDashBoard(Action<DashboardStatus> updater)
    {
        var dashboardStatus = _cache.Get<DashboardStatus>(HUB_NAME_KEY);
        if (dashboardStatus == null) dashboardStatus = await CreateStatus();
        updater(dashboardStatus);
        _cache.Set(HUB_NAME_KEY, dashboardStatus);
        await _hub.Clients.All.SendAsync("update", dashboardStatus);
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
            TotalAvailable = await _db.Users.Select(e => e.Available).SumAsync(),
            TotalProfit = await _db.Users.Select(e => e.Profit).SumAsync(),
            Transactions = await _db.PaymentTransactions.LongCountAsync(),
            CampaignReported = await _db.AbuseReports.LongCountAsync(),
            TotalFee = await _db.PaymentTransactions.Where(e=>e.TransactionType == TransactionTypeEnum.Fee).SumAsync(e=>Math.Abs(e.Amount)),
            LastModified = DateTime.UtcNow
        };
    }


}