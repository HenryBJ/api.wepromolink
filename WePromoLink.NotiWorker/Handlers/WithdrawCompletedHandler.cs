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

public class WithdrawCompletedHandler : IRequestHandler<WithdrawCompletedEvent, bool>
{
    private readonly IServiceScopeFactory _fac;
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly IPushService _pushService;
    public WithdrawCompletedHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard, IPushService pushService, IServiceScopeFactory fac)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
        _pushService = pushService;
        _fac = fac;
    }
    public async Task<bool> Handle(WithdrawCompletedEvent request, CancellationToken cancellationToken)
    {
        await _pushService.SetPushNotification(request.UserId, e => e.Transaction++);
        
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        // Notificamos del Deposito
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Etag = Nanoid.Nanoid.Generate(size: 12),
            Title = "Withdraw completed",
            Message = $"We are pleased to inform you that your withdrawl request has been successfully processed. An amount of {request.Amount.ToString("C")} USD has been debited from your account and transferred to your Stripe connected account.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();
        
        // Enviamos un correo
        _senderEmail.Send(request.Name!, request.Email!, "Withdraw completed", Templates.Withdraw(new { user = request.Name, amount = request.Amount.ToString("C"), year = DateTime.Now.Year.ToString() })).GetAwaiter().GetResult();
        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = 0,
            Deposit = 0,
            GeoLocations = 0,
            Hits = 0,
            RegisteredUsers = 0,
            Shareds = 0,
            TotalAvailable = 0,
            TotalProfit = 0,
            Transactions = 0,
            Withdraw = 1,
            CampaignReported = 0,
            TotalFee = request.Fee ?? 0
        });
        return true;
    }
}