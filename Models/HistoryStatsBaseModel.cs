namespace WePromoLink.Models;

public abstract class HistoryStatsBaseModel<T, R> : StatsBaseModel
{
    public string? Title { get; set; }
    public T? X0 { get; set; }
    public T? X1 { get; set; }
    public T? X2 { get; set; }
    public T? X3 { get; set; }
    public T? X4 { get; set; }
    public T? X5 { get; set; }
    public T? X6 { get; set; }
    public T? X7 { get; set; }
    public T? X8 { get; set; }
    public T? X9 { get; set; }
    public R? L0 { get; set; }
    public R? L1 { get; set; }
    public R? L2 { get; set; }
    public R? L3 { get; set; }
    public R? L4 { get; set; }
    public R? L5 { get; set; }
    public R? L6 { get; set; }
    public R? L7 { get; set; }
    public R? L8 { get; set; }
    public R? L9 { get; set; }

}