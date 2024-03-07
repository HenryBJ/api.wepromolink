using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Campaign;

public class ReduceAvailableCommandHandler : ChartDataRepository<string, decimal>, IProcessEvent<ReduceAvailableCommand>
{
    private const int MAX_ITEMS = 20;

    public ReduceAvailableCommandHandler(IMongoClient client) : base(StatisticsEnum.Available, client)
    {
    }

    public async Task<bool> Process(ReduceAvailableCommand item)
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
                  backgroundColor = new List<string>{"rgb(251,237,213)"},
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