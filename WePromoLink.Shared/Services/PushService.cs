using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WePromoLink.Data;
using WePromoLink.DTO.PushNotification;
using WePromoLink.Services.Cache;

namespace WePromoLink.Services;

public class PushService : IPushService
{
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<PushService> _logger;
    private readonly IShareCache _cache;

    private readonly IServiceScopeFactory _fac;

    public PushService(IHttpContextAccessor httpContextAccessor, ILogger<PushService> logger, IShareCache cache, IServiceScopeFactory fac)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _cache = cache;
        _fac = fac;
        var scope = _fac.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<DataContext>();
    }

    public async Task<PushNotification> GetPushNotification()
    {
        try
        {
            var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
            string key = $"push_{firebaseId}";

            if (_cache.TryGetValue<PushNotification>(key, out PushNotification push))
            {
                _cache.Set(key, new PushNotification
                {
                    Campaign = 0,
                    Etag = push.Etag,
                    Links = 0,
                    Messages = new List<string>(),
                    Notification = 0,
                    Transaction = 0
                });
                _logger.LogInformation($"push key: {key} etag: {push.Etag}");
                return push;
            }
            else
            {
                PushNotification emptyPush = new()
                {
                    Campaign = 0,
                    Etag = Nanoid.Nanoid.Generate(size: 12),
                    Links = 0,
                    Messages = new List<string>(),
                    Notification = 0,
                    Transaction = 0
                };

                _logger.LogWarning("Push etag empty");
                _cache.Set(key, emptyPush);
                return emptyPush;
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    // This method does not get call from client only from server
    // public async Task SetPushNotification(string firebaseId, PushNotification newPush)
    // {
    //     string key = $"push_{firebaseId}";
    //     _cache.Set(key, newPush);
    // }

    // This method does not get call from client only from server
    // public async Task SetPushNotification(Guid UserId, PushNotification newPush)
    // {
    //     var firebaseId = await _db.Users.Where(e => e.Id == UserId).Select(e => e.FirebaseId).SingleAsync();
    //     if (String.IsNullOrEmpty(firebaseId)) throw new Exception("Empty firebaseId");
    //     await SetPushNotification(firebaseId, newPush);
    // }

    // This method does not get call from client only from server
    public async Task SetPushNotification(string firebaseId, Action<PushNotification> pushReducer)
    {
        _logger.LogInformation($"SetPushNotification : {firebaseId}");
        
        string key = $"push_{firebaseId}";
        if (_cache.TryGetValue(key, out PushNotification push))
        {
            pushReducer(push!);
            push!.Etag = Nanoid.Nanoid.Generate(size: 12);
            _cache.Set(key, push);
        }
        else
        {
            push = new PushNotification
            {
                Campaign = 0,
                Links = 0,
                Transaction = 0,
                Messages = new List<string>(),
                Notification = 0
            };
            pushReducer(push);
            push.Etag = Nanoid.Nanoid.Generate(size: 12);
            _cache.Set(key, push);
        }
    }

    // This method does not get call from client only from server
    public async Task SetPushNotification(Guid UserId, Action<PushNotification> pushReducer)
    {
        var firebaseId = await _db.Users.Where(e => e.Id == UserId).Select(e => e.FirebaseId).SingleAsync();
        if (String.IsNullOrEmpty(firebaseId)) throw new Exception("Empty firebaseId");
        await SetPushNotification(firebaseId, pushReducer);
    }

}