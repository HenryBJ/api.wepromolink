namespace WePromoLink.Models;

public class JoinWaitingListModel 
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public JoinWaitingListModel()
    {
        CreatedAt = DateTime.UtcNow;
    }
}