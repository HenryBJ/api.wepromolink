using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignDeletedHandler : IRequestHandler<CampaignDeletedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly DataContext _db;
    public CampaignDeletedHandler(IEmailSender senderEmail, DataContext db)
    {
        _senderEmail = senderEmail;
        _db = db;
    }
    public Task<bool> Handle(CampaignDeletedEvent request, CancellationToken cancellationToken)
    {
        //Create a Notification
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Title = "Campaign deleted",
            Message = $"Your campaign called '{request.CampaignName}' has been deleted, remaining campaign budget has been added to the available balance",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();

        return Task.FromResult(true);
    }
}