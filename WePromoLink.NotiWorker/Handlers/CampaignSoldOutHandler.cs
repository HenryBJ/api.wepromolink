using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignSoldOutHandler : IRequestHandler<CampaignSoldOutEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly DataContext _db;
    public CampaignSoldOutHandler(IEmailSender senderEmail, DataContext db)
    {
        _senderEmail = senderEmail;
        _db = db;
    }
    public Task<bool> Handle(CampaignSoldOutEvent request, CancellationToken cancellationToken)
    {
        // Crear Notificacion
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Title = "Campaign deactivated",
            Message = $"Your campaign called '{request.CampaignName}' has been deactivated due insufficient budget (${request.Amount.ToString("0.00")} USD)",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        return Task.FromResult(true);
    }
}