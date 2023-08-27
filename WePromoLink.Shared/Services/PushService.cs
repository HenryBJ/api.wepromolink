using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    public PushService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<PushService> logger, IShareCache cache)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _cache = cache;
    }

    public async Task<PushNotification> GetPushNotification()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);

        string key = $"push_{firebaseId}";
        if (_cache.TryGetValue(key, out PushNotification push))
        {
            _logger.LogInformation("Read from cache");
            return push;
        }

        var pushModel = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Push).SingleAsync();
        push = new PushNotification
        {
            Campaign = pushModel.Campaign,
            Clicks = pushModel.Clicks,
            Deposit = pushModel.Deposit,
            Etag = pushModel.Etag,
            Links = pushModel.Links,
            Messages = new List<string>(),
            Notification = pushModel.Notification,
            Withdraw = pushModel.Withdraw
        };
        _cache.Set(key, push);
        _logger.LogInformation("Read from DB");
        return push;
    }

    // This method does not get call from client only from server
    public async Task SetPushNotification(string firebaseId, PushNotification newPush)
    {
        string key = $"push_{firebaseId}";
        var pushModel = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Push).SingleAsync();

        newPush.Etag = Nanoid.Nanoid.Generate(size: 12);

        pushModel.Campaign = newPush.Campaign;
        pushModel.Clicks = newPush.Clicks;
        pushModel.Deposit = newPush.Deposit;
        pushModel.Etag = newPush.Etag;
        pushModel.LastModified = DateTime.UtcNow;
        pushModel.Links = newPush.Links;
        pushModel.Notification = newPush.Notification;
        pushModel.Withdraw = newPush.Withdraw;

        _db.Pushes.Update(pushModel);
        await _db.SaveChangesAsync();
        _cache.Set(key, newPush);
    }

    // This method does not get call from client only from server
    public async Task SetPushNotification(Guid UserId, PushNotification newPush)
    {
        var firebaseId = await _db.Users.Where(e => e.Id == UserId).Select(e => e.FirebaseId).SingleAsync();
        if (String.IsNullOrEmpty(firebaseId)) throw new Exception("Empty firebaseId");
        await SetPushNotification(firebaseId, newPush);
    }

    // This method does not get call from client only from server
    public async Task SetPushNotification(string firebaseId, Action<PushNotification> pushReducer)
    {
        string key = $"push_{firebaseId}";
        if (_cache.TryGetValue(key, out PushNotification push))
        {
            pushReducer(push);
            push.Etag = Nanoid.Nanoid.Generate(size: 12);
            await SetPushNotification(firebaseId, push);
        }
        else
        {
            var pushModel = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Push).SingleAsync();
            push = new PushNotification
            {
                Campaign = pushModel.Campaign,
                Clicks = pushModel.Clicks,
                Deposit = pushModel.Deposit,
                Etag = pushModel.Etag,
                Links = pushModel.Links,
                Messages = new List<string>(),
                Notification = pushModel.Notification,
                Withdraw = pushModel.Withdraw
            };
            pushReducer(push);
            push.Etag = Nanoid.Nanoid.Generate(size: 12);
            _cache.Set(key, push);
            await SetPushNotification(firebaseId, push);
        }
    }

    // This method does not get call from client only from server
    public async Task SetPushNotification(Guid UserId, Action<PushNotification> pushReducer)
    {
        var firebaseId = await _db.Users.Where(e => e.Id == UserId).Select(e => e.FirebaseId).SingleAsync();
        if (String.IsNullOrEmpty(firebaseId)) throw new Exception("Empty firebaseId");
        await SetPushNotification(firebaseId, pushReducer);
    }

    public async Task<PushNotification> UpdatePushNotification(PushNotification newNotification)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        string key = $"push_{firebaseId}";
        var pushModel = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Push).SingleAsync();

        newNotification.Etag = Nanoid.Nanoid.Generate(size: 12);

        pushModel.Campaign = newNotification.Campaign;
        pushModel.Clicks = newNotification.Clicks;
        pushModel.Deposit = newNotification.Deposit;
        pushModel.Etag = newNotification.Etag;
        pushModel.LastModified = DateTime.UtcNow;
        pushModel.Links = newNotification.Links;
        pushModel.Notification = newNotification.Notification;
        pushModel.Withdraw = newNotification.Withdraw;

        _db.Pushes.Update(pushModel);
        await _db.SaveChangesAsync();
        _cache.Set(key, newNotification);
        return newNotification;
    }
}