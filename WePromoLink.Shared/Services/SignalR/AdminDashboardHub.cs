using Microsoft.Azure.SignalR.Management;
using Newtonsoft.Json;
using WePromoLink.DTO.SignalR;
using WePromoLink.Services.Cache;

namespace WePromoLink.Services.SignalR;

public class AdminDashboardHub : IAdminDashboardHub, IDisposable
{
    private const string HUB_NAME = "wepromolink_admin";
    private const string HUB_NAME_KEY = "wepromolink_admin_key";
    private readonly string _connectionString;
    private ServiceHubContext? hubContext;
    private readonly IShareCache _cache;
    public AdminDashboardHub(string connectionString, IShareCache cache)
    {
        _connectionString = connectionString;
        _cache = cache;
        Connect();
    }

    private void Connect()
    {
        var serviceManager = new ServiceManagerBuilder().WithOptions(option =>
         {
             option.ConnectionString = _connectionString;
             option.ServiceTransportType = ServiceTransportType.Transient;
         }).BuildServiceManager();
        hubContext = serviceManager.CreateHubContextAsync(HUB_NAME, CancellationToken.None).Result;
    }

    // Docs: https://github.com/aspnet/AzureSignalR-samples/blob/main/samples/Management/MessagePublisher/README.md

    public async Task UpdateDashBoard(Action<DashboardStatus> updater)
    {
        var dashboardStatus = _cache.Get<DashboardStatus>(HUB_NAME_KEY);
        if (dashboardStatus == null) dashboardStatus = new DashboardStatus();
        updater(dashboardStatus);
        _cache.Set(HUB_NAME_KEY, dashboardStatus);
        await hubContext!.Clients.All.SendCoreAsync("update", new[] { dashboardStatus });
    }

    public void Dispose()
    {
        hubContext?.Dispose();
    }
}