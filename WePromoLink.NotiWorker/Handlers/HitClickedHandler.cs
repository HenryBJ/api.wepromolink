using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.SignalR;
using WePromoLink.Services.Email;
using WePromoLink.Services.SignalR;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class HitClickedHandler : IRequestHandler<HitClickedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    public HitClickedHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
    }
    public Task<bool> Handle(HitClickedEvent request, CancellationToken cancellationToken)
    {
        
        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = 0,
            Deposit = 0,
            GeoLocations = 0,
            Hits = 1,
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