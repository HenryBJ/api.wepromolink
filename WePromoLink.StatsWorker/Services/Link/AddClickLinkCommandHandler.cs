using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Link;

public class AddClickLinkCommandHandler : ChartDataRepository<string, int>, IProcessEvent<AddClickLinkCommand>
{

    public AddClickLinkCommandHandler(IMongoClient client) : base(StatisticsEnum.LinkXClick, client)
    {
    }

    public async Task<bool> Process(AddClickLinkCommand item)
    {
        try
        {
            if (Exists(item.ExternalId))
            {
                await UpdateChartData(item.ExternalId, old =>
                {
                    if (DateTime.Parse(old.labels.Last()).Date == DateTime.UtcNow.Date)
                    {
                        old.datasets[0].data[old.datasets[0].data.Count - 1] += 1;
                    }
                    else
                    if (DateTime.Parse(old.labels.Last()).Date < DateTime.UtcNow.Date)
                    {
                        old.labels.Add(DateTime.UtcNow.Date.ToShortDateString());
                        var lastvalue = old.datasets[0].data.Last();
                        old.datasets[0].data.Add(lastvalue + 1);
                    }
                    return old;
                });
            }
            else
            {
                InsertChartData(new ChartData<string, int>
                {
                    _id = item.ExternalId,
                    labels = new List<string> { item.CreatedAt.Date.ToShortDateString() },
                    datasets = new List<Dataset<int>>{new Dataset<int>
                {
                  backgroundColor = new List<string>{"rgb(234,114,39)"},
                  borderColor = new List<string>{"rgb(249,115,22)"},
                  borderWidth = 1,
                  data = new List<int>{1},
                  label = "Clicks"
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