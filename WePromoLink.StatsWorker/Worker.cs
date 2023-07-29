using WePromoLink.Data;
using WePromoLink.Repositories;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.StatsWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 5;
    private readonly ILogger<Worker> _logger;
    private MessageBroker<UpdateCampaignMessage> _campaignMessageBroker;
    private MessageBroker<UpdateUserMessage> _userMessageBroker;
    private MessageBroker<UpdateLinkMessage> _linkMessageBroker;
    private readonly IServiceScopeFactory _fac;

    public Worker(IServiceScopeFactory fac,
    ILogger<Worker> logger,
    MessageBroker<UpdateCampaignMessage> campaignMessageBroker,
    MessageBroker<UpdateUserMessage> userMessageBroker,
    MessageBroker<UpdateLinkMessage> linkMessageBroker)
    {
        _logger = logger;
        _campaignMessageBroker = campaignMessageBroker;
        _userMessageBroker = userMessageBroker;
        _linkMessageBroker = linkMessageBroker;
        _fac = fac;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _campaignMessageBroker.Receive((item) => UpdateCampaign(item).Result);
        await _userMessageBroker.Receive((item) => UpdateUser(item).Result);
        await _linkMessageBroker.Receive((item) => UpdateLink(item).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> UpdateLink(UpdateLinkMessage item)
    {

        using (var scope = _fac.CreateScope())
        {
            DataContext _db = scope.ServiceProvider.GetRequiredService<DataContext>();
            DataRepository _repo = new DataRepository(_db);
            var exist = _db.Links.Any(e => e.Id == item.Id);
            if (!exist)
            {
                _logger.LogWarning($"Updating link: {item.Id} not found");
                return false;
            }

            using (var dbtrans = _db.Database.BeginTransaction())
            {
                try
                {
                    await _repo.UpdateLink(item.Id);
                    dbtrans.Commit();
                    return true;
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message);
                    dbtrans.Rollback();
                    return false;
                }
            }
        }
    }

    private async Task<bool> UpdateUser(UpdateUserMessage item)
    {
        using (var scope = _fac.CreateScope())
        {
            DataContext _db = scope.ServiceProvider.GetRequiredService<DataContext>();
            DataRepository _repo = new DataRepository(_db);
            var exist = _db.Users.Any(e => e.Id == item.Id);
            if (!exist)
            {
                _logger.LogWarning($"Updating User: {item.Id} not found");
                return false;
            }

            using (var dbtrans = _db.Database.BeginTransaction())
            {
                try
                {
                    await _repo.UpdateUser(item.Id);
                    dbtrans.Commit();
                    return true;
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message);
                    dbtrans.Rollback();
                    return false;
                }
            }
        }


    }

    private async Task<bool> UpdateCampaign(UpdateCampaignMessage item)
    {
        using (var scope = _fac.CreateScope())
        {
            DataContext _db = scope.ServiceProvider.GetRequiredService<DataContext>();
            DataRepository _repo = new DataRepository(_db);
            var exist = _db.Campaigns.Any(e => e.Id == item.Id);
            if (!exist)
            {
                _logger.LogWarning($"Updating Campaign: {item.Id} not found");
                return false;
            }

            using (var dbtrans = _db.Database.BeginTransaction())
            {
                try
                {
                    await _repo.UpdateCampaign(item.Id);
                    dbtrans.Commit();
                    return true;
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message);
                    dbtrans.Rollback();
                    return false;
                }
            }
        }
    }
}
