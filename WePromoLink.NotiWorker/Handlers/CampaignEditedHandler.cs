using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignEditedHandler : IRequestHandler<CampaignEditedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly IServiceScopeFactory _fac;
    private readonly IPushService _pushService;
    public CampaignEditedHandler(IEmailSender senderEmail, IServiceScopeFactory fac, IPushService pushService)
    {
        _senderEmail = senderEmail;
        _fac = fac;
        _pushService = pushService;
    }
    public Task<bool> Handle(CampaignEditedEvent request, CancellationToken cancellationToken)
    {
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        _pushService.SetPushNotification(request.UserId, e => e.Notification++);
        //Create a Notification
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Etag = Nanoid.Nanoid.Generate(size:12),
            Title = "Campaign edited",
            Message = $"Your campaign called '{request.CampaignNameNew}' has been successfully edited. It has been assigned a new budget of {request.AmountNew.ToString("0.00")} USD.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        return Task.FromResult(true);
    }
}