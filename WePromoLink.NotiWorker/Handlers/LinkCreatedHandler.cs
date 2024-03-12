using MediatR;
using WePromoLink.DTO.Events;
using WePromoLink.Services;
using WePromoLink.Services.Email;

namespace WePromoLink.Handlers;

public class LinkCreatedHandler : IRequestHandler<LinkCreatedEvent, bool>
{
    private readonly IEmailSender _senderEmail;
    private readonly IPushService _pushService;
    public LinkCreatedHandler(IEmailSender senderEmail, IPushService pushService)
    {
        _senderEmail = senderEmail;
        _pushService = pushService;
    }
    public async Task<bool> Handle(LinkCreatedEvent request, CancellationToken cancellationToken)
    {
        await _pushService.SetPushNotification(request.LinkCreatorUserId, e => e.Links++);
        return true;
    }
}