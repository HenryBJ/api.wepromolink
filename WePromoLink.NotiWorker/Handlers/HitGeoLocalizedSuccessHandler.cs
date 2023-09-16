using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.SignalR;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class HitGeoLocalizedSuccessHandler : IRequestHandler<HitGeoLocalizedSuccessEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly IPushService _pushService;
    public HitGeoLocalizedSuccessHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard, IPushService pushService)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
        _pushService = pushService;
    }
    public Task<bool> Handle(HitGeoLocalizedSuccessEvent request, CancellationToken cancellationToken)
    {

        _pushService.SetPushNotification(request.UserId, e =>
        {
            e.Messages ??= new List<string>();
            e.Messages.Add($"Click from <img src=\"{request.FlagUrl}\" alt=\"country\"> to campaign <b>{request.CampaignName}</b> &#x1F973;");
        });

        _pushService.SetPushNotification(request.LinkOwnerId, e =>
        {
            e.Messages ??= new List<string>();
            e.Messages.Add($"Click from <img src=\"{request.FlagUrl}\" alt=\"country\"> to your link of <b>{request.CampaignName}</b> &#x1F4B0;");
        });

        if (request.FirstTime)
            _senderDashboard.Send(new DashboardStatus
            {
                Clicks = 0,
                CampaignBudget = 0,
                Campaigns = 0,
                Deposit = 0,
                GeoLocations = 1,
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