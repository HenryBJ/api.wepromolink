using MongoDB.Driver;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.Statistics;
using WePromoLink.Enums;

namespace WePromoLink.StatsWorker.Services.Campaign;

public class AddClickCountryCampaignCommandHandler : ChartDataRepository<string, int>, IProcessEvent<AddClickCountryCampaignCommand>
{

    public AddClickCountryCampaignCommandHandler(IMongoClient client) : base(StatisticsEnum.CampaignClickCountry, client)
    {
    }

    public async Task<bool> Process(AddClickCountryCampaignCommand item)
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
                        old.datasets[0].backgroundColor.Add(GenerateColor());
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
                  backgroundColor = new List<string>{GenerateColor()},
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

    private string GenerateColor()
    {
        Random random = new Random();
        int r = random.Next(50, 256); // Valor rojo entre 50 y 255
        int g = random.Next(50, 256); // Valor verde entre 50 y 255
        int b = random.Next(50, 256); // Valor azul entre 50 y 255

        return $"rgb({r},{g},{b})";
    }
}