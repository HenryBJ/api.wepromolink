using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.SignalR;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class UserRegisteredHandler : IRequestHandler<UserRegisteredEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    public UserRegisteredHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
    }
    public Task<bool> Handle(UserRegisteredEvent request, CancellationToken cancellationToken)
    {
        _senderEmail.Send(request.Name, request.Email, "Welcome to WePromoLink", Templates.Welcome(new { user = request.Name })).GetAwaiter().GetResult();
        _senderDashboard.Send(new DashboardStatus
        {
            Clicks = 0,
            CampaignBudget = 0,
            Campaigns = 0,
            Deposit = 0,
            GeoLocations = 0,
            Hits = 0,
            RegisteredUsers = 1,
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
