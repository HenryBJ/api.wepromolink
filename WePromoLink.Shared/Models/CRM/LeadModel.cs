using WePromoLink.Enums.CRM;

namespace WePromoLink.Models.CRM;

public class LeadModel 
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool EmailVerified { get; set; }
    public string? Industry { get; set; }
    public string? Website { get; set; }
    public string? Sector { get; set; }
    public string? Languaje { get; set; }
    public string? Country { get; set; }
    public string Status { get; set; }
    public string? Note { get; set; }
    public string? CampaginOrigin { get; set; }
    public DateTime CreatedAt { get; set; }

    public LeadModel()
    {
        Id = Guid.NewGuid();
        ExternalId = Nanoid.Nanoid.Generate(size:12);
        CreatedAt = DateTime.UtcNow;
    }
}