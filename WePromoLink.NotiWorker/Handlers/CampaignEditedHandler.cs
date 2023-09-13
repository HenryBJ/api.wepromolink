using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignEditedHandler : IRequestHandler<CampaignEditedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly IServiceScopeFactory _fac;
    public CampaignEditedHandler(IEmailSender senderEmail, IServiceScopeFactory fac)
    {
        _senderEmail = senderEmail;
        _fac = fac;
    }
    public Task<bool> Handle(CampaignEditedEvent request, CancellationToken cancellationToken)
    {
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        
        //Create a Notification
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Title = "Campaign edited",
            Message = $"Your campaign called '{request.CampaignNameNew}' has been successfully edited. It has been assigned a new budget of {request.AmountNew.ToString("0.00")} USD.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        return Task.FromResult(true);
    }
}