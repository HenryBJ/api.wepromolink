using WePromoLink.Data;
namespace WePromoLink.Shared.DTO.Messages;

public abstract class UpdateStatsMessage
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public UpdateStatsMessage()
    {
        CreatedAt = DateTime.UtcNow;
    }
}