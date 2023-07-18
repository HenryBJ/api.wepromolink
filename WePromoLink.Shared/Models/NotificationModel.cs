namespace WePromoLink.Models;

public class NotificationModel: StatsBaseModel
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; }
    public UserModel User { get; set; }
    public Guid UserModelId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
}