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

public class CampaignSharedHandler : IRequestHandler<CampaignSharedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly IServiceScopeFactory _fac;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly IPushService _pushService;
    public CampaignSharedHandler(IEmailSender senderEmail, IServiceScopeFactory fac, IPushService pushService, MessageBroker<DashboardStatus> senderDashboard)
    {
        _senderEmail = senderEmail;
        _fac = fac;
        _pushService = pushService;
        _senderDashboard = senderDashboard;
    }
    public Task<bool> Handle(CampaignSharedEvent request, CancellationToken cancellationToken)
    {
        _pushService.SetPushNotification(request.OwnerUserId, e =>
        {
            e.Notification++;
            if (e.Messages == null) e.Messages = new List<string>();
            e.Messages.Add($"<b>{request.SharedByName}</b> shared your campaign <b>{request.CampaignName}</b> &#x1F389;");
        });


        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

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

        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = 0,
            Deposit = 0,
            GeoLocations = 0,
            Hits = 0,
            RegisteredUsers = 0,
            Shareds = 1,
            TotalAvailable = 0,
            TotalProfit = 0,
            Transactions = 0,
            UnVerifiedUsers = 0,
            VerifiedUsers = 0,
            Withdraw = 0,
            CampaignReported = 0,
        });

        return Task.FromResult(true);
    }
}