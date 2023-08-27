using System.Net;
using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands;
using WePromoLink.Enums;
using WePromoLink.Models;
using WePromoLink.Repositories;
using WePromoLink.Services;
using WePromoLink.Shared.DTO.Messages;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.HitWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 3;
    private readonly DataContext _db;
    private readonly IPStackService _service;
    private readonly ILogger<Worker> _logger;
    private MessageBroker<Hit> _messageBroker;
    private MessageBroker<UpdateCampaignMessage> _campaignMessageBroker;
    private MessageBroker<UpdateUserMessage> _userMessageBroker;
    private MessageBroker<UpdateLinkMessage> _linkMessageBroker;
    private readonly MessageBroker<BaseEvent> _eventSender;
    private readonly MessageBroker<GeoLocalizeHitCommand> _commandSender;


    public Worker(
        IServiceScopeFactory fac,
        ILogger<Worker> logger,
        MessageBroker<UpdateCampaignMessage> campaignMessageBroker,
        MessageBroker<UpdateUserMessage> userMessageBroker,
        MessageBroker<UpdateLinkMessage> linkMessageBroker,
        MessageBroker<BaseEvent> eventSender,
        MessageBroker<GeoLocalizeHitCommand> commandSender)
    {
        var scope = fac.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        _messageBroker = scope.ServiceProvider.GetRequiredService<MessageBroker<Hit>>();
        _logger = logger;
        _service = scope.ServiceProvider.GetRequiredService<IPStackService>();
        _campaignMessageBroker = campaignMessageBroker;
        _userMessageBroker = userMessageBroker;
        _linkMessageBroker = linkMessageBroker;
        _eventSender = eventSender;
        _commandSender = commandSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        await _messageBroker.Receive((hit) => ProcessHit(hit).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> ProcessHit(Hit item)
    {
        using (var dbtrans = _db.Database.BeginTransaction())
        {
            try
            {
                if (item.Origin == null) throw new Exception("Invalid Origen IP Address");
                var originIP = IPAddress.Parse(item.Origin);

                if (IPAddress.IsLoopback(originIP))
                {
                    originIP = IPAddress.Parse("170.75.168.146");
                }
                else
                {
                    originIP = originIP.MapToIPv4();
                }

                var origin = originIP.ToString();

                var link = await _db.Links
                .Include(e => e.User)
                .ThenInclude(e => e.Profit)
                .Include(e => e.Campaign)
                .ThenInclude(e => e.User)
                .Where(e => e.ExternalId == item.LinkId)
                .SingleOrDefaultAsync();
                if (link == null) return false;

                var userFromLink = link.User;
                var profitFromLink = link.User.Profit;
                var userFromCampaign = link.Campaign.User;
                var campaign = link.Campaign;
                var linkId = link.Id;
                var hit = await _db.Hits.Where(e => e.LinkModelId == linkId && e.Origin == origin).SingleOrDefaultAsync();

                // Actualizamos el HIT
                if (hit != null)
                {
                    hit.Counter++;
                    hit.LastHitAt = DateTime.UtcNow;

                    _db.Hits.Update(hit);
                    await _db.SaveChangesAsync();
                    dbtrans.Commit();

                    _eventSender.Send(new LinkClickedEvent
                    {
                        LinkId = link.Id,
                        CampaignId = link.Campaign.Id,
                        CampaignName = link.Campaign.Title,
                        OwnerName = link.Campaign.User.Fullname,
                        LinkCreatorName = link.User.Fullname,
                        UserId = link.User.Id
                    });

                    _eventSender.Send(new HitClickedEvent
                    {
                        LinkId = link.Id,
                        CampaignId = link.Campaign.Id,
                        CampaignName = link.Campaign.Title,
                        OwnerName = link.Campaign.User.Fullname,
                        LinkCreatorName = link.User.Fullname,
                        UserId = link.User.Id,
                        IsCountable = false
                    });

                    return true;
                }

                if (!campaign.Status)
                {
                    _logger.LogInformation("Hit on a deactivate campaign");
                    return true;
                }
                else if (campaign.Budget < (10 / 1000)) // menos de un click
                {
                    _logger.LogInformation("Hit on a campaign with minimun budget, turn off campaign");
                    campaign.Status = false;
                    campaign.LastUpdated = DateTime.UtcNow;
                    _db.Campaigns.Update(campaign);
                    await _db.SaveChangesAsync();
                    dbtrans.Commit();

                    _eventSender.Send(new CampaignSoldOutEvent
                    {
                        CampaignId = link.Campaign.Id,
                        CampaignName = link.Campaign.Title,
                        UserId = link.User.Id,
                        Amount = link.Campaign.Budget,
                        EPM = link.Campaign.EPM,
                        Name = link.User.Fullname
                    });
                    return true;
                }

                bool lastHit = campaign.Budget <= (campaign.EPM / 1000);
                decimal amount = lastHit ? campaign.Budget : (campaign.EPM / 1000);

                // Creamos el HIT
                HitModel model = new HitModel
                {
                    Id = Guid.NewGuid(),
                    LinkModelId = linkId,
                    Counter = 1,
                    CreatedAt = DateTime.UtcNow,
                    IsGeolocated = false,
                    LastHitAt = DateTime.UtcNow,
                    FirstHitAt = DateTime.UtcNow,
                    Origin = origin
                };

                
                _db.Hits.Add(model);
                await _db.SaveChangesAsync();

                // Actualizamos el Budget de la Campanna
                campaign.Budget -= amount;
                campaign.LastUpdated = DateTime.UtcNow;
                _db.Campaigns.Update(campaign);
                await _db.SaveChangesAsync();

                // Actualizamos el profit del usuario del link
                profitFromLink.Value += amount;
                profitFromLink.LastModified = DateTime.UtcNow;
                profitFromLink.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                _db.Profits.Update(profitFromLink);
                await _db.SaveChangesAsync();

                // Creamos el PaymentTransaction para el usuario del link
                var paymentA = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = amount,
                    LinkModelId = linkId,
                    CreatedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow,
                    ExternalId = Nanoid.Nanoid.Generate(size: 12),
                    Status = TransactionStatusEnum.Completed,
                    TransactionType = TransactionTypeEnum.Profit,
                    UserModelId = link.UserModelId,
                    Title = "Click Profit"
                };

                // Creamos el PaymentTransaction para el usuario de la Campanna
                var paymentB = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = -amount,
                    CampaignModelId = campaign.Id,
                    CreatedAt = DateTime.UtcNow,
                    CompletedAt = DateTime.UtcNow,
                    ExternalId = Nanoid.Nanoid.Generate(size: 12),
                    Status = TransactionStatusEnum.Completed,
                    TransactionType = TransactionTypeEnum.ClickCost,
                    UserModelId = campaign.UserModelId,
                    Title = "Click Cost"
                };

                await _db.PaymentTransactions.AddAsync(paymentA);
                await _db.PaymentTransactions.AddAsync(paymentB);
                await _db.SaveChangesAsync();

                // Actualizamos las estadisticas
                _campaignMessageBroker.Send(new UpdateCampaignMessage { Id = campaign.Id });
                _userMessageBroker.Send(new UpdateUserMessage { Id = userFromLink.Id });
                _userMessageBroker.Send(new UpdateUserMessage { Id = userFromCampaign.Id });
                _linkMessageBroker.Send(new UpdateLinkMessage { Id = link.Id });

                dbtrans.Commit();

                _commandSender.Send(new GeoLocalizeHitCommand { HitId = model.Id });

                _eventSender.Send(new LinkClickedEvent
                {
                    LinkId = link.Id,
                    CampaignId = link.Campaign.Id,
                    CampaignName = link.Campaign.Title,
                    OwnerName = link.Campaign.User.Fullname,
                    LinkCreatorName = link.User.Fullname,
                    UserId = link.User.Id
                });

                _eventSender.Send(new HitCreatedEvent
                {
                    LinkId = link.Id,
                    CampaignId = link.Campaign.Id,
                    CampaignName = link.Campaign.Title,
                    OwnerName = link.Campaign.User.Fullname,
                    LinkCreatorName = link.User.Fullname,
                    UserId = link.User.Id
                });

                _eventSender.Send(new HitClickedEvent
                {
                    LinkId = link.Id,
                    CampaignId = link.Campaign.Id,
                    CampaignName = link.Campaign.Title,
                    OwnerName = link.Campaign.User.Fullname,
                    LinkCreatorName = link.User.Fullname,
                    UserId = link.User.Id,
                    IsCountable = true
                });

                _eventSender.Send(new CampaignClickedEvent
                {
                    CampaignId = link.Campaign.Id,
                    CampaignName = link.Campaign.Title,
                    UserId = link.User.Id,
                    Amount = link.Campaign.Budget,
                    EPM = link.Campaign.EPM,
                    Name = link.User.Fullname
                });

                return true;
            }
            catch (System.Exception ex)
            {
                dbtrans.Rollback();
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}