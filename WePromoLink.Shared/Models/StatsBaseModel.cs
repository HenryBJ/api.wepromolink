namespace WePromoLink.Models;

public abstract class StatsBaseModel 
{
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public TimeSpan? MaxAge { get; set; }
    public string? Etag { get; set; }

    public StatsBaseModel()
    {
        CreatedAt = DateTime.UtcNow;
        LastModified = DateTime.UtcNow;
        ExpiredAt = DateTime.UtcNow.AddSeconds(20);
        MaxAge = TimeSpan.FromSeconds(20);
        Etag = Nanoid.Nanoid.Generate(size:12);        
    }
}