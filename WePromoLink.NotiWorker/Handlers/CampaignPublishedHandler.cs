using MediatR;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class CampaignPublishedHandler : IRequestHandler<CampaignPublishedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly IServiceScopeFactory _fac;
    private readonly IPushService _pushService;
    public CampaignPublishedHandler(IEmailSender senderEmail, IPushService pushService, IServiceScopeFactory fac)
    {
        _senderEmail = senderEmail;
        _pushService = pushService;
        _fac = fac;
    }
    public Task<bool> Handle(CampaignPublishedEvent request, CancellationToken cancellationToken)
    {
        using var scope = _fac.CreateScope();
        var _db = scope.ServiceProvider.GetRequiredService<DataContext>();

        var listIds = _db.Users.Select(e => e.FirebaseId).ToList();

        foreach (var item in listIds)
        {
            _pushService.SetPushNotification(item!, e => e.Campaign++);
        }


        return Task.FromResult(true);
    }
}