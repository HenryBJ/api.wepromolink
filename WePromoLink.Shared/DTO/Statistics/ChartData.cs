namespace WePromoLink.DTO.Statistics;

public class ChartData<TLabel,TValue>
{
    public string _id { get; set; }
    public List<TLabel> labels { get; set; }
    public List<Dataset<TValue>> datasets { get; set; }

    public ChartData()
    {
        labels = new List<TLabel>();
        datasets = new List<Dataset<TValue>>();
    }
}
