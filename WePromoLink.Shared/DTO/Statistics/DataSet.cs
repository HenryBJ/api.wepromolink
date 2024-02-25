namespace WePromoLink.DTO.Statistics;

public class Dataset<TValue>
{
    public string label { get; set; }
    public List<TValue> data { get; set; }
    public List<string> backgroundColor { get; set; }
    public List<string> borderColor { get; set; }
    public int borderWidth { get; set; }
}