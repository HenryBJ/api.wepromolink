namespace WePromoLink.Models.CRM;

public class CampaignRunner 
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public string SenderEmail { get; set; }
    public string Description { get; set; }
    public int TotalLeads { get; set; }
    public int Converted { get; set; }
    public int TotalUnsubscribe { get; set; }
    public string Name { get; set; }
    public string ConfigurationJSONUrl { get; set; }
    public DateTime CreatedAt { get; set; }

    public CampaignRunner()
    {
        Id = Guid.NewGuid();
        ExternalId = Nanoid.Nanoid.Generate(size:12);
        CreatedAt = DateTime.UtcNow;
    }
}

