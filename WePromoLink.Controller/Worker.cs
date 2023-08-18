using WePromoLink.Controller.Tasks;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Controller;

public class Worker : BackgroundService
{
    const int WAIT_TIME_MIN = 30;
    private readonly ILogger<Worker> _logger;
    private readonly UpdateStatsTask _updateStatsTask;
    private readonly CleanImagesTask _cleanImagesTask;

    public Worker(ILogger<Worker> logger, UpdateStatsTask updateStatsTask, CleanImagesTask cleanImagesTask)
    {
        _logger = logger;
        _updateStatsTask = updateStatsTask;
        _cleanImagesTask = cleanImagesTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await _updateStatsTask.Update();
            await _cleanImagesTask.Update();
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromMinutes(WAIT_TIME_MIN), stoppingToken);
        }
    }
}
