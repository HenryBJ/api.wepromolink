using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.SignalR;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class DepositCompletedHandler : IRequestHandler<DepositCompletedEvent, bool>
{
    private readonly IServiceScopeFactory _fac;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly IEmailSender _senderEmail;
    private readonly IPushService _pushService;
    public DepositCompletedHandler(IEmailSender senderEmail, IServiceScopeFactory fac, MessageBroker<DashboardStatus> senderDashboard, IPushService pushService)
    {
        _senderEmail = senderEmail;
        _fac = fac;
        _senderDashboard = senderDashboard;
        _pushService = pushService;
    }
    public async Task<bool> Handle(DepositCompletedEvent request, CancellationToken cancellationToken)
    {
        await _pushService.SetPushNotification(request.UserId, e => { e.Transaction++; e.Notification++; });
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        // Notificamos del Deposito
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Etag = Nanoid.Nanoid.Generate(size:12),
            Title = "Deposit completed",
            Message = $"We are pleased to inform you that your deposit has been successfully processed. An amount of {request.Amount.ToString("C")} USD has been credited to your account.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();

        // Enviamos un correo
        _senderEmail.Send(request.Name!, request.Email!, "Deposit completed", Templates.Deposit(new { user = request.Name, amount = request.Amount.ToString("C"), year = DateTime.Now.Year.ToString() })).GetAwaiter().GetResult();

        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = 0,
            Deposit = 1,
            GeoLocations = 0,
            Hits = 0,
            RegisteredUsers = 0,
            Shareds = 0,
            TotalAvailable = 0,
            TotalProfit = 0,
            Transactions = 0,
            UnVerifiedUsers = 0,
            VerifiedUsers = 0,
            Withdraw = 0,
            CampaignReported = 0,
        });

        return true;
    }
}