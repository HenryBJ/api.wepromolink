namespace WePromoLink.DTO;

public class HistoricalData<T>
{
    public string? Title { get; set; }
    public List<string> Labels { get; set; }
    public List<List<T>> Data { get; set; }
    public List<string> DataLabels { get; set; }

}