namespace WePromoLink.Models;

public class ProfileModel
{
    public Guid Id { get; set; }
    public virtual UserModel UserModel { get; set; }
    public Guid UserModelId { get; set; }
    public string? ImageUrl { get; set; }
    public string ImageHeaderUrl { get; set; }
    public string? Bio { get; set; }
    public string Social { get; set; }
    public string MyPageId { get; set; }

}