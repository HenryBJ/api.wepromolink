using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands.Statistics;
using WePromoLink.DTO.SignalR;
using WePromoLink.Services;
using WePromoLink.Services.Email;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.Handlers;

public class HitGeoLocalizedSuccessHandler : IRequestHandler<HitGeoLocalizedSuccessEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly MessageBroker<DashboardStatus> _senderDashboard;
    private readonly MessageBroker<StatsBaseCommand> _sender;
    private readonly IPushService _pushService;
    private readonly IServiceScopeFactory _fac;
    public HitGeoLocalizedSuccessHandler(IEmailSender senderEmail, MessageBroker<DashboardStatus> senderDashboard, IPushService pushService, MessageBroker<StatsBaseCommand> sender, IServiceScopeFactory fac)
    {
        _senderEmail = senderEmail;
        _senderDashboard = senderDashboard;
        _pushService = pushService;
        _sender = sender;
        _fac = fac;
    }
    public async Task<bool> Handle(HitGeoLocalizedSuccessEvent request, CancellationToken cancellationToken)
    {

        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        _sender.Send(new AddClickCountryLinkCommand
        {
            Country = request.Country!,
            ExternalId = _db.Links.Where(e => request.LinkId == e.Id).Select(e => e.ExternalId).First()
        });

        _sender.Send(new AddClickCountryCampaignCommand
        {
            Country = request.Country!,
            ExternalId = _db.Campaigns.Where(e => request.CampaignId == e.Id).Select(e => e.ExternalId).First()
        });

        await _pushService.SetPushNotification(request.UserId, e =>
        {
            e.Messages ??= new List<string>();
            e.Messages.Add($"Click from <img style=\"display:inline\" src=\"{request.FlagUrl}\" alt=\"country\" width=\"20\" height=\"auto\" style=\"vertical-align: middle;\"> to campaign <b>{request.CampaignName}</b> &#x1F973;");
        });

        await _pushService.SetPushNotification(request.LinkOwnerId, e =>
        {
            e.Messages ??= new List<string>();
            e.Messages.Add($"Click from <img style=\"display:inline\" src=\"{request.FlagUrl}\" alt=\"country\" width=\"20\" height=\"auto\" style=\"vertical-align: middle;\"> to your link of <b>{request.CampaignName}</b> &#x1F4B0;");
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
                Withdraw = 0,
                CampaignReported = 0,
                TotalFee = 0
            });
        return true;
    }
}