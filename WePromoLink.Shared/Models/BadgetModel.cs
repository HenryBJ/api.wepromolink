namespace WePromoLink.Models;

public class BadgetModel
{
    public Guid Id { get; set; }
    public Guid UserModelId { get; set; }
    public UserModel User { get; set; }
    public int Notification { get; set; }
    public int Campaign { get; set; }
    public int Link { get; set; }
    public int Clicks { get; set; }
    public int Deposit { get; set; }
    public int Withdraw { get; set; }
    public string flag { get; set; }
    
    public DateTime? LastModified { get; set; }

}