using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Campaign;

public class AddClick : ChartDataRepository<string, int>, IProcessEvent<AddClickCommand>
{

    public AddClick(IMongoClient client) : base(StatisticsEnum.CampaignXClick, client)
    {
    }

    public async Task<bool> Process(AddClickCommand item)
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
                        old.datasets[0].data.Add(1);
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
                  backgroundColor = new List<string>{"rgb(251,237,213)"},
                  borderColor = new List<string>{"rgb(251,237,213)"},
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