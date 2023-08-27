using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignSharedHandler : IRequestHandler<CampaignSharedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly DataContext _db;
    public CampaignSharedHandler(IEmailSender senderEmail, DataContext db)
    {
        _senderEmail = senderEmail;
        _db = db;
    }
    public Task<bool> Handle(CampaignSharedEvent request, CancellationToken cancellationToken)
    {
        //Create a Notification
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.SharedByUserId,
            Title = "Campaign shared",
            Message = $"A link has been created to your campaign called '{request.CampaignName}' by the user {request.SharedByName}",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        return Task.FromResult(true);
    }
}