namespace WePromoLink.Models;

public class MyPageModel
{
    public Guid Id { get; set; }
    public UserModel UserModel { get; set; }
    public Guid UserModelId { get; set; }

    public bool Active { get; set; }
    public int Visited { get; set; }
    public int Conversion { get; set; }
    public string CallOfAction { get; set; }
    public string QRUrl { get; set; }
    public string ImageHeaderUrl { get; set; }
    public string Text { get; set; }
    public string Title { get; set; }
    public string Template { get; set; }

    public MyPageModel()
    {
        Id = Guid.NewGuid();
    }    

}