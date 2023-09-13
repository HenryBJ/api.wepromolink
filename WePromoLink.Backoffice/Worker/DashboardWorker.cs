
using WePromoLink.DTO.SignalR;
using WePromoLink.Services.SignalR;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Backoffice.Worker;

public class DashboardWorker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 3;
    private readonly MessageBroker<DashboardStatus> _sender;
    private readonly AdminDashboardService _dashboard;
    private readonly ILogger<DashboardWorker> _logger;
    public DashboardWorker(MessageBroker<DashboardStatus> sender, AdminDashboardService dashboard, ILogger<DashboardWorker> logger)
    {
        _sender = sender;
        _dashboard = dashboard;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _sender.Receive((item) => ProcessEvent(item).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> ProcessEvent(DashboardStatus ev)
    {
        try
        {
            _logger.LogInformation($"Recibido dashboardStatus");
            await _dashboard.UpdateDashBoard(e =>
            {
                e.CampaignBudget += ev.CampaignBudget;
                e.GeoLocations += ev.GeoLocations;
                e.Campaigns += ev.Campaigns;
                e.Clicks += ev.Clicks;
                e.Deposit += ev.Deposit;
                e.Hits += ev.Hits;
                e.RegisteredUsers += ev.RegisteredUsers;
                e.Shareds += ev.Shareds;
                e.TotalAvailable += ev.TotalAvailable;
                e.TotalProfit += ev.TotalProfit;
                e.Transactions += ev.Transactions;
                e.UnVerifiedUsers += ev.UnVerifiedUsers;
                e.VerifiedUsers += ev.VerifiedUsers;
                e.Withdraw += ev.Withdraw;
                e.CampaignReported += ev.CampaignReported;
                e.LastModified = DateTime.UtcNow;
            });
            return true;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }

    }
}