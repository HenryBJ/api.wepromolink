using WePromoLink.Controller.Tasks;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Controller;

public class Worker : BackgroundService
{
    const int WAIT_TIME_MIN = 10;//TODO: 30
    private readonly ILogger<Worker> _logger;
    private readonly CleanImagesTask _cleanImagesTask;
    private readonly CampaignEmailRunnerTask _emailRunnerTask;

    public Worker(ILogger<Worker> logger, CleanImagesTask cleanImagesTask, CampaignEmailRunnerTask emailRunnerTask)
    {
        _logger = logger;
        _cleanImagesTask = cleanImagesTask;
        _emailRunnerTask = emailRunnerTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await _emailRunnerTask.Update();
            await _cleanImagesTask.Update();
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromMinutes(WAIT_TIME_MIN), stoppingToken);
        }
    }
}
