namespace WePromoLink;

public class MyCampaignList
{
    public List<MyCampaign> Items { get; set; }

    public Pagination Pagination { get; set;}

    public MyCampaignList()
    {
        Pagination = new Pagination();        
    }
}