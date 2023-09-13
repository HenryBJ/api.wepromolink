using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.SignalR;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class CampaignDeletedHandler : IRequestHandler<CampaignDeletedEvent, bool>
{
    private readonly IEmailSender _senderEmail;

    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly IServiceScopeFactory _fac;
    public CampaignDeletedHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
    }
    public Task<bool> Handle(CampaignDeletedEvent request, CancellationToken cancellationToken)
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
            Title = "Campaign deleted",
            Message = $"Your campaign called '{request.CampaignName}' has been deleted, remaining campaign budget has been added to the available balance",
        };
        _db.Notifications.Add(noti);
        _db.SaveChanges();

        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = -1,
            Deposit = 0,
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

        return Task.FromResult(true);
    }
}