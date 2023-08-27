using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignCreatedHandler : IRequestHandler<CampaignCreatedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly DataContext _db;
    public CampaignCreatedHandler(IEmailSender senderEmail, DataContext db)
    {
        _senderEmail = senderEmail;
        _db = db;
    }
    public Task<bool> Handle(CampaignCreatedEvent request, CancellationToken cancellationToken)
    {
        //Create a Notification
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Title = "Campaign created",
            Message = $"Your campaign called '{request.CampaignName}' has been successfully created. It has been assigned a budget of {request.InitialAmount.ToString("0.00")} USD.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        return Task.FromResult(true);
    }
}