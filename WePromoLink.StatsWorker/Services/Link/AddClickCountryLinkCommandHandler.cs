using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Campaign;

public class AddClickCountryLinkCommandHandler : ChartDataRepository<string, int>, IProcessEvent<AddClickCountryLinkCommand>
{

    public AddClickCountryLinkCommandHandler(IMongoClient client) : base(StatisticsEnum.LinkClickCountry, client)
    {
    }

    public async Task<bool> Process(AddClickCountryLinkCommand item)
    {
        try
        {
            if (Exists(item.ExternalId))
            {
                await UpdateChartData(item.ExternalId, old =>
                {
                    if(old.labels.Contains(item.Country))
                    {
                        int pos = old.labels.IndexOf(item.Country);
                        old.datasets[0].data[pos]+=1;
                    }
                    else
                    {
                        old.labels.Add(item.Country);
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
                    labels = new List<string> { item.Country },
                    datasets = new List<Dataset<int>>{new Dataset<int>
                {
                  backgroundColor = new List<string>{"rgb(251,237,213)"},
                  borderColor = new List<string>{"rgb(249,115,22)"},
                  borderWidth = 1,
                  data = new List<int>{1},
                  label = "Clicks by country"
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