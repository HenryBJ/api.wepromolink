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

public class CampaignCreatedHandler : IRequestHandler<CampaignCreatedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly IServiceScopeFactory _fac;
    private readonly IPushService _pushService;
    public CampaignCreatedHandler(IEmailSender senderEmail, IServiceScopeFactory fac, MessageBroker<DashboardStatus> senderDashboard, IPushService pushService)
    {
        _senderEmail = senderEmail;
        _fac = fac;
        _senderDashboard = senderDashboard;
        _pushService = pushService;
    }
    public async Task<bool> Handle(CampaignCreatedEvent request, CancellationToken cancellationToken)
    {
        await _pushService.SetPushNotification(request.UserId, e => e.Notification++);
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        //Create a Notification
        var noti = new NotificationModel
        {
            Id = Guid.NewGuid(),
            ExternalId = Nanoid.Nanoid.GenerateAsync(size: 12).GetAwaiter().GetResult(),
            Status = NotificationStatusEnum.Unread,
            UserModelId = request.UserId,
            Etag = Nanoid.Nanoid.Generate(size:12),
            Title = "Campaign created",
            Message = $"Your campaign called '{request.CampaignName}' has been successfully created. It has been assigned a budget of ${request.InitialAmount.ToString("0.00")} USD.",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();

        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = 1,
            Deposit = 0,
            GeoLocations = 0,
            Hits = 0,
            RegisteredUsers = 0,
            Shareds = 0,
            TotalAvailable = 0,
            TotalProfit = 0,
            Transactions = 0,
            Withdraw = 0,
            CampaignReported = 0,
            TotalFee = 0
        });

        return true;
    }
}