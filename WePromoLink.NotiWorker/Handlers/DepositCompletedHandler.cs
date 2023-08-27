using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class DepositCompletedHandler : IRequestHandler<DepositCompletedEvent, bool>
{
    private readonly DataContext _db;
    private readonly IEmailSender _senderEmail;
    public DepositCompletedHandler(IEmailSender senderEmail, DataContext db)
    {
        _senderEmail = senderEmail;
        _db = db;
    }
    public Task<bool> Handle(DepositCompletedEvent request, CancellationToken cancellationToken)
    {
        // Notificamos del Deposito
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Title = "Deposit completed",
            Message = $"We are pleased to inform you that your deposit has been successfully processed. An amount of ${request.Amount} USD has been credited to your account.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();

        // Enviamos un correo
        _senderEmail.Send(request.Name!, request.Email!, "Deposit completed", Templates.Deposit(new { user = request.Name, amount = request.Amount.ToString("C") })).GetAwaiter().GetResult();

        return Task.FromResult(true);
    }
}