using MongoDB.Driver;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.StatsWorker.Services.Campaign;
using WePromoLink.StatsWorker.Services.Link;

namespace WePromoLink.StatsWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 5;
    private readonly ILogger<Worker> _logger;
    private MessageBroker<AddClickCampaignCommand> _addclick;
    private MessageBroker<StatsBaseCommand> _eventMessageBroker;
    private readonly IMongoClient _mongo;

    private readonly IServiceScopeFactory _fac;

    public Worker(IServiceScopeFactory fac,
    ILogger<Worker> logger,
    IMongoClient mongo,
    MessageBroker<StatsBaseCommand> eventMessageBroker)
    {
        _logger = logger;
        _fac = fac;
        _mongo = mongo;
        _eventMessageBroker = eventMessageBroker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _eventMessageBroker.ReceiveBaseEvent((item) => ProcessEvent(item).Result);
        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> ProcessEvent(StatsBaseCommand ev)
    {
        try
        {
            _logger.LogInformation($"Recibido {ev.EventType}");
            switch (ev)
            {
                case AddClickCampaignCommand addClickCampaignCommand: return new AddClickCampaignCommandHandler(_mongo).Process(addClickCampaignCommand).Result;
                case AddAvailableCommand addAvailableCommand: return new AddAvailableCommandHandler(_mongo).Process(addAvailableCommand).Result;
                case ReduceAvailableCommand reduceAvailableCommand: return new ReduceAvailableCommandHandler(_mongo).Process(reduceAvailableCommand).Result;
                case AddProfitCommand addProfitCommand: return new AddProfitCommandHandler(_mongo).Process(addProfitCommand).Result;
                case ReduceProfitCommand reduceProfitCommand: return new ReduceProfitCommandHandler(_mongo).Process(reduceProfitCommand).Result;
                case AddGeneralClickLinkCommand addGeneralClickLinkCommand: return new AddGeneralClickLinkCommandHandler(_mongo).Process(addGeneralClickLinkCommand).Result;
                case AddGeneralClickCampaignCommand addGeneralClickCampaignCommand: return new AddGeneralClickCampaignCommandHandler(_mongo).Process(addGeneralClickCampaignCommand).Result;
                case AddGeneralShareCommand addGeneralShareCommand: return new AddGeneralShareCommandHandler(_mongo).Process(addGeneralShareCommand).Result;
                case AddShareCampaignCommand addShareCampaignCommand: return new AddShareCampaignCommandHandler(_mongo).Process(addShareCampaignCommand).Result;
                case AddClickCountryCampaignCommand addClickCountryCampaignCommand: return new AddClickCountryCampaignCommandHandler(_mongo).Process(addClickCountryCampaignCommand).Result;
                case AddSpendCampaignCommand addSpendCampaignCommand: return new AddSpendCampaignCommandHandler(_mongo).Process(addSpendCampaignCommand).Result;
                case ReduceBudgetCampaignCommand reduceBudgetCampaignCommand: return new ReduceBudgetCampaignCommandHandler(_mongo).Process(reduceBudgetCampaignCommand).Result;
                case AddBudgetCampaignCommand addBudgetCampaignCommand: return new AddBudgetCampaignCommandHandler(_mongo).Process(addBudgetCampaignCommand).Result;
                case AddClickLinkCommand addClickLinkCommand: return new AddClickLinkCommandHandler(_mongo).Process(addClickLinkCommand).Result;
                case AddClickCountryLinkCommand addClickCountryLinkCommand: return new AddClickCountryLinkCommandHandler(_mongo).Process(addClickCountryLinkCommand).Result;
                case AddProfitLinkCommand addProfitLinkCommand: return new AddProfitLinkCommandHandler(_mongo).Process(addProfitLinkCommand).Result;
                
                
                default: return false;
            }

        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            return false;
        }
    }
}
