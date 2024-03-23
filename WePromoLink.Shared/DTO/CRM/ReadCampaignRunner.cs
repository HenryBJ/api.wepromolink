namespace WePromoLink.DTO.CRM;
public class ReadCampaignRunner 
{
    public string Name { get; set; }
    public string ConfigurationJSONUrl { get; set; }
    public string ExternalId { get; set; }
    public string SenderEmail { get; set; }
    public int Converted { get; set; }
    public int TotalLead { get; set; }
    public int TotalUnSubscribe { get; set; }

}