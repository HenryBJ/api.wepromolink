namespace WePromoLink.DTO.PushNotification;

public class PushNotification
{
    public int Notification { get; set; }
    public int Campaign { get; set; }
    public int Links { get; set; }
    public int Transaction { get; set; }
    public List<string>? Messages { get; set; }
    public string Etag { get; set; }
}