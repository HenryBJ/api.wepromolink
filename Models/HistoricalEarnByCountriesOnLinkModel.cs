namespace WePromoLink.Models;

public class HistoryEarnByCountriesOnLinkModel:HistoryStatsBaseModel<decimal, string>
{
    public Guid Id { get; set; }
    public virtual  LinkModel Link { get; set; }
    public Guid LinkModelId { get; set; }
}