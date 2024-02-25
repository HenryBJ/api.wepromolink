namespace WePromoLink.Models;

public class NotificationModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
    public string Etag { get; set; }
    public DateTime ExpiredAt { get; set; }
    public DateTime LastModified { get; set; }
    public DateTime CreatedAt { get; set; }
}