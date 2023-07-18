using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;
using WePromoLink;
using WePromoLink.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace WePromoLink.Services;

public class NotificationService : INotificationService
{
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<NotificationService> _logger;
    public NotificationService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<NotificationService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task Delete(string id)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");

        var noti = await _db.Notifications.Where(e=>e.ExternalId == id).SingleOrDefaultAsync();
        if(noti == null) throw new Exception("Notification not found");

        _db.Notifications.Remove(noti);
        await _db.SaveChangesAsync();
    }

    public async Task<PaginationList<Notification>> Get(int? page, int? cant)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");

        PaginationList<Notification> list = new PaginationList<Notification>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var counter = await _db.Notifications
        .Where(e => e.UserModelId == user.Id)
        .CountAsync();

        list.Items = await _db.Notifications
        .Where(e => e.UserModelId == user.Id)
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new Notification
        {
            Id = e.ExternalId,
            Read = e.Status == NotificationStatusEnum.Read,
            CreatedAt = e.CreatedAt,
            Status = e.Status,
            Title = e.Title
        })
        .ToListAsync();

        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    public async Task<NotificationDetail> GetDetails(string id)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");

        var result = await _db.Notifications
        .Where(e => e.ExternalId == id)
        .Select(e => new NotificationDetail
        {
            Id = e.ExternalId,
            Read = e.Status == NotificationStatusEnum.Read,
            CreatedAt = e.CreatedAt,
            Status = e.Status,
            Title = e.Title,
            Message = e.Message
        })
        .SingleOrDefaultAsync();
        if (result == null) throw new Exception("Notification no found");

        return result;
    }

    public async Task MarkAsRead(string id)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");

        var noti = await _db.Notifications.Where(e=>e.ExternalId == id).SingleOrDefaultAsync();
        if(noti == null) throw new Exception("Notification not found");

        noti.Status = NotificationStatusEnum.Read;
        noti.LastModified = DateTime.UtcNow;
        noti.Etag = await Nanoid.Nanoid.GenerateAsync(size:12);

        _db.Notifications.Update(noti);
        await _db.SaveChangesAsync();
    }
    
}