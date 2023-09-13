using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.SignalR;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class WithdrawCompletedHandler : IRequestHandler<WithdrawCompletedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    public WithdrawCompletedHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
    }
    public Task<bool> Handle(WithdrawCompletedEvent request, CancellationToken cancellationToken)
    {
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
            UnVerifiedUsers = 0,
            VerifiedUsers = 0,
            Withdraw = 1,
            CampaignReported = 0,
        });
        return Task.FromResult(true);
    }
}