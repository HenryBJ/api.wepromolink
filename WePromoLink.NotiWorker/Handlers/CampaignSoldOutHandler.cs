using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignSoldOutHandler : IRequestHandler<CampaignSoldOutEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly IServiceScopeFactory _fac;
    private readonly IPushService _pushService;
    public CampaignSoldOutHandler(IEmailSender senderEmail, IPushService pushService)
    {
        _senderEmail = senderEmail;
        _pushService = pushService;
    }
    public Task<bool> Handle(CampaignSoldOutEvent request, CancellationToken cancellationToken)
    {

        _pushService.SetPushNotification(request.UserId, e => e.Notification++);

        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        // Crear Notificacion
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Etag = Nanoid.Nanoid.Generate(size:12),
            Title = "Campaign deactivated",
            Message = $"Your campaign called '{request.CampaignName}' has been deactivated due insufficient budget (${request.Amount.ToString("0.00")} USD)",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        return Task.FromResult(true);
    }
}