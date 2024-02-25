using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;
using WePromoLink.DTO.Events;
using WePromoLink.DTO.Events.Commands;
using WePromoLink.Services;
using WePromoLink.Shared.RabbitMQ;

namespace WePromoLink.GeoLocationWorker;

public class Worker : BackgroundService
{
    const int WAIT_TIME_SECONDS = 8;
    private readonly DataContext _db;
    private readonly IPStackService _service;
    private readonly ILogger<Worker> _logger;
    private readonly MessageBroker<GeoLocalizeHitCommand> _commandBroker;
    private readonly MessageBroker<BaseEvent> _eventBroker;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory fac, MessageBroker<GeoLocalizeHitCommand> commandBroker, MessageBroker<BaseEvent> eventBroker)
    {
        var scope = fac.CreateScope();
        _logger = logger;
        _db = scope.ServiceProvider.GetRequiredService<DataContext>();
        _service = scope.ServiceProvider.GetRequiredService<IPStackService>();;
        _commandBroker = commandBroker;
        _eventBroker = eventBroker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _commandBroker.Receive((command) => ProcessCommand(command).Result);

        while (true)
        {
            stoppingToken.ThrowIfCancellationRequested();
            await Task.Delay(TimeSpan.FromSeconds(WAIT_TIME_SECONDS), stoppingToken);
        }
    }

    private async Task<bool> ProcessCommand(GeoLocalizeHitCommand command)
    {
        var hit = _db.Hits
        .Include(e => e.Link)
        .ThenInclude(e => e.Campaign)
        .ThenInclude(e => e.User)
        .Where(e => e.Id == command.HitId).SingleOrDefault();
        if (hit == null)
        {
            _logger.LogError("Hit does no exists");
            return true;
        } 

        if (hit.IsGeolocated)
        {
            _logger.LogWarning("Trying to geolocalize a hit again !!!");
            return true;
        }

        var geoData = _db.GeoDatas.Where(e => e.IP == hit.Origin).SingleOrDefault();
        if (geoData == null)
        {
            try
            {
                geoData = await _service.Locate(hit.Origin!);
                if (geoData == null) throw new Exception("GeoData empty");
                if (string.IsNullOrEmpty(geoData.Country)) throw new Exception("Country empty");


                await _db.GeoDatas.AddAsync(geoData);
                hit.IsGeolocated = true;
                hit.GeolocatedAt = DateTime.UtcNow;
                hit.GeoData = geoData;

                _db.Hits.Update(hit);
                await _db.SaveChangesAsync();

                _eventBroker.Send(new HitGeoLocalizedSuccessEvent
                {
                    CampaignId = hit.Link.Campaign.Id,
                    CampaignName = hit.Link.Campaign.Title,
                    LinkId = hit.Link.Id,
                    UserId = hit.Link.Campaign.User.Id,
                    OwnerName = hit.Link.Campaign.User.Fullname,
                    Country = geoData.Country,
                    FlagUrl = geoData.CountryFlagUrl,
                    Latitud = geoData.Latitude,
                    Longitud = geoData.Longitude,
                    FirstTime = true,
                    LinkOwnerId = _db.Hits.Where(e=>e.Id == hit.Id).Select(e=>e.Link.UserModelId).Single()
                });
                return true;
            }
            catch (System.Exception ex)
            {
                _eventBroker.Send(new HitGeoLocalizedFailureEvent
                {
                    Attempt = 1,
                    CampaignId = hit.Link.Campaign.Id,
                    CampaignName = hit.Link.Campaign.Title,
                    FailureReason = ex.Message,
                    LinkId = hit.Link.Id,
                    UserId = hit.Link.Campaign.User.Id,
                    OwnerName = hit.Link.Campaign.User.Fullname
                });
                return true;
            }
        }
        else
        {
            hit.GeoData = geoData;
            hit.IsGeolocated = true;
            hit.GeolocatedAt = DateTime.UtcNow;
            _db.Hits.Update(hit);
            await _db.SaveChangesAsync();

            _eventBroker.Send(new HitGeoLocalizedSuccessEvent
            {
                CampaignId = hit.Link.Campaign.Id,
                CampaignName = hit.Link.Campaign.Title,
                LinkId = hit.Link.Id,
                UserId = hit.Link.Campaign.User.Id,
                OwnerName = hit.Link.Campaign.User.Fullname,
                Country = geoData.Country,
                FlagUrl = geoData.CountryFlagUrl,
                Latitud = geoData.Latitude,
                Longitud = geoData.Longitude,
                LinkOwnerId = _db.Hits.Where(e=>e.Id == hit.Id).Select(e=>e.Link.UserModelId).Single()
            });
            return true;
        }
    }
}
