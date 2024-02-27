using MongoDB.Driver;
using WePromoLink.Data;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.StatsWorker.Services.Campaign;

namespace WePromoLink.StatsWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 5;
    private readonly ILogger<Worker> _logger;
    private MessageBroker<AddClickCampaignCommand> _addclick;
    private readonly IMongoClient _mongo;

    private readonly IServiceScopeFactory _fac;

    public Worker(IServiceScopeFactory fac,
    ILogger<Worker> logger,
    MessageBroker<AddClickCampaignCommand> addclick,
    IMongoClient mongo)
    {
        _logger = logger;
        _fac = fac;
        _addclick = addclick;
        _mongo = mongo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _addclick.Receive((item) => new AddClickCampaignCommandHandler(_mongo).Process(item).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

}
