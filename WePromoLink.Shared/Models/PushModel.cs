using WePromoLink.Enums;

namespace WePromoLink.Models;

public class PushModel
{
    public Guid Id { get; set; }
    public virtual UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public int Notification { get; set; }
    public int Campaign { get; set; }
    public int Links { get; set; }
    public int Clicks { get; set; }
    public int Deposit { get; set; }
    public int Withdraw { get; set; }
    public string Etag { get; set; }
    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    public PushModel()
    {
        Etag = Nanoid.Nanoid.Generate(size:12);
    }
}