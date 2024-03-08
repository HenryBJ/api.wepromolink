using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Campaign;

public class ReduceProfitCommandHandler : ChartDataRepository<string, decimal>, IProcessEvent<ReduceProfitCommand>
{
    private const int MAX_ITEMS = 20;

    public ReduceProfitCommandHandler(IMongoClient client) : base(StatisticsEnum.Profit, client)
    {
    }

    public async Task<bool> Process(ReduceProfitCommand item)
    {
        try
        {
            if (Exists(item.ExternalId))
            {
                await UpdateChartData(item.ExternalId, old =>
                {
                    if (DateTime.Parse(old.labels.Last()).Date == DateTime.UtcNow.Date)
                    {
                        old.datasets[0].data[old.datasets[0].data.Count - 1] -= Math.Round(item.Amount, 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    if (DateTime.Parse(old.labels.Last()).Date < DateTime.UtcNow.Date)
                    {
                        if(old.datasets.Count>=MAX_ITEMS)
                        {
                            old.datasets.RemoveAt(0);
                            old.labels.RemoveAt(0);
                        }
                        
                        old.labels.Add(DateTime.UtcNow.Date.ToShortDateString());
                        var lastvalue = old.datasets[0].data.Last();
                        old.datasets[0].data.Add(lastvalue+old.datasets[0].data[old.datasets[0].data.Count - 1]-Math.Round(item.Amount, 2, MidpointRounding.AwayFromZero));
                    }
                    return old;
                });
            }
            else
            {
                InsertChartData(new ChartData<string, decimal>
                {
                    _id = item.ExternalId,
                    labels = new List<string> { item.CreatedAt.Date.ToShortDateString() },
                    datasets = new List<Dataset<decimal>>{new Dataset<decimal>
                {
                  backgroundColor = new List<string>{"rgb(234,114,39)"},
                  borderColor = new List<string>{"rgb(249,115,22)"},
                  borderWidth = 1,
                  data = new List<decimal>{-Math.Round(item.Amount,2,MidpointRounding.AwayFromZero)},
                  label = "Money available"
                }}
                });
            }
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}