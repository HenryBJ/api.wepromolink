using System.Globalization;
using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Campaign;

public class AddShareCampaignCommandHandler : ChartDataRepository<string, int>, IProcessEvent<AddShareCampaignCommand>
{
    private const int MAX_ITEMS = 20;

    public AddShareCampaignCommandHandler(IMongoClient client) : base(StatisticsEnum.CampaignXShare, client)
    {
    }

    public async Task<bool> Process(AddShareCampaignCommand item)
    {
        try
        {
            if (Exists(item.ExternalId))
            {
                await UpdateChartData(item.ExternalId, old =>
                {
                    if (DateTime.Parse(old.labels.Last(), new CultureInfo("es-ES")).Date == DateTime.UtcNow.Date)
                    {
                        old.datasets[0].data[old.datasets[0].data.Count - 1] += 1;
                    }
                    else
                    if (DateTime.Parse(old.labels.Last(), new CultureInfo("es-ES")).Date < DateTime.UtcNow.Date)
                    {
                        if(old.datasets.Count>=MAX_ITEMS)
                        {
                            old.datasets.RemoveAt(0);
                            old.labels.RemoveAt(0);
                        }

                        old.labels.Add(DateTime.UtcNow.Date.ToString("d", new CultureInfo("es-ES")));
                        var lastvalue = old.datasets[0].data.Last();
                        old.datasets[0].data.Add(lastvalue+1);
                    }
                    return old;
                });
            }
            else
            {
                InsertChartData(new ChartData<string, int>
                {
                    _id = item.ExternalId,
                    labels = new List<string> { item.CreatedAt.Date.ToString("d", new CultureInfo("es-ES")) },
                    datasets = new List<Dataset<int>>{new Dataset<int>
                {
                  backgroundColor = new List<string>{"rgb(234,114,39)"},
                  borderColor = new List<string>{"rgb(249,115,22)"},
                  borderWidth = 1,
                  data = new List<int>{1},
                  label = "number of shared"
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