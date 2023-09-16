using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;
using WePromoLink.Enums;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using WePromoLink.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using WePromoLink.Services.Cache;
using WePromoLink.Shared.RabbitMQ;
using WePromoLink.DTO.Events;

namespace WePromoLink.Services;

public class CampaignService : ICampaignService
{
    private readonly BTCPaymentService _client;
    private readonly IOptions<BTCPaySettings> _options;
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CampaignService> _logger;
    private readonly IShareCache _cache;
    private readonly MessageBroker<BaseEvent> _eventSender;
    public CampaignService(DataContext db, IOptions<BTCPaySettings> options, BTCPaymentService client, IHttpContextAccessor httpContextAccessor, ILogger<CampaignService> logger, IShareCache cache, MessageBroker<BaseEvent> eventSender)
    {
        _db = db;
        _options = options;
        _client = client;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _cache = cache;
        _eventSender = eventSender;
    }

    public async Task<string> CreateCampaign(Campaign campaign)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();

        if (user == null) throw new Exception("User does not exits");
        if (user.Available.Value < campaign.Budget) throw new Exception("Insufficient balance");
        if (!user.IsSubscribed) throw new Exception("Subscription is not active");
        if (user.IsBlocked) throw new Exception("User is blocked");

        var externalId = await Nanoid.Nanoid.GenerateAsync(size: 12);
        var available = user.Available;

        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                //Fetchear la image y vincularla
                var imageData = _db.ImageDatas.Where(e => e.ExternalId == campaign.ImageBundleId).SingleOrDefault();
                if (imageData != null)
                {
                    imageData.Bound = true;
                    _db.ImageDatas.Update(imageData);
                }

                var item = new CampaignModel
                {
                    Id = Guid.NewGuid(),
                    Budget = campaign.Budget,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    Description = campaign.Description,
                    EPM = campaign.EPM,
                    ExternalId = externalId,
                    ImageDataModelId = imageData?.Id,
                    Title = campaign.Title,
                    Url = campaign.Url,
                    IsArchived = false,
                    IsBlocked = false,
                    SharedLastWeekOnCampaign = new SharedLastWeekOnCampaignModel(),
                    Status = campaign.Budget >= 10,
                    SharedTodayOnCampaignModel = new SharedTodayOnCampaignModel(),
                    UserModelId = user.Id,
                    ClicksLastWeekOnCampaign = new ClicksLastWeekOnCampaignModel(),
                    ClicksTodayOnCampaign = new ClicksTodayOnCampaignModel(),
                    HistoryClicksByCountriesOnCampaign = new HistoryClicksByCountriesOnCampaignModel(),
                    HistoryClicksOnCampaign = new HistoryClicksOnCampaignModel(),
                    HistorySharedByUsersOnCampaign = new HistorySharedByUsersOnCampaignModel(),
                    HistorySharedOnCampaign = new HistorySharedOnCampaignModel()
                };

                // Validate Available balance    
                available.Value = available.Value - campaign.Budget;
                available.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                _db.Availables.Update(available);
                await _db.SaveChangesAsync();
                if (available.Value < 0) throw new Exception("Negative balance");

                // Add Campaign
                await _db.Campaigns.AddAsync(item);
                await _db.SaveChangesAsync();

                // Create Payment Transaction
                var paymentTrans = new PaymentTransaction
                {
                    Id = Guid.NewGuid(),
                    Amount = campaign.Budget,
                    CampaignModelId = item.Id,
                    CompletedAt = DateTime.UtcNow.AddSeconds(1),
                    CreatedAt = DateTime.UtcNow,
                    ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                    Status = TransactionStatusEnum.Completed,
                    Title = $"Campaign {item.Title} created",
                    TransactionType = TransactionTypeEnum.CreateCampaign,
                    UserModelId = user.Id
                };
                await _db.PaymentTransactions.AddAsync(paymentTrans);
                await _db.SaveChangesAsync();

                transaction.Commit();

                _eventSender.Send(new CampaignCreatedEvent
                {
                    CampaignId = item.Id,
                    CampaignName = item.Title,
                    EPM = item.EPM,
                    InitialAmount = item.Budget,
                    Name = user.Fullname,
                    UserId = user.Id
                });

                if (campaign.Budget >= 10)
                {
                    _eventSender.Send(new CampaignPublishedEvent
                    {
                        CampaignId = item.Id,
                        Amount = item.Budget,
                        CampaignName = item.Title,
                        Name = user.Fullname,
                        UserId = user.Id
                    });
                }

                return externalId;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Invalid Data");
            }
        }

    }

    public async Task Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User no found");
        if (user.IsBlocked) throw new Exception("User is blocked");
        if (!user.IsSubscribed) throw new Exception("User is not subscribed");

        var campaignModel = await _db.Campaigns.Where(e => e.ExternalId == id).SingleOrDefaultAsync();
        if (campaignModel == null) throw new Exception("Campaign does not exits");
        if (campaignModel.IsArchived) throw new Exception("Campaign deleted");
        var available = user.Available;

        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                if (campaignModel.Budget > 0)
                {
                    available.Value += campaignModel.Budget;
                    available.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                    _db.Availables.Update(available);
                    await _db.SaveChangesAsync();

                    // Create Payment Transaction
                    var paymentTrans = new PaymentTransaction
                    {
                        Id = Guid.NewGuid(),
                        Amount = campaignModel.Budget,
                        CampaignModelId = campaignModel.Id,
                        CompletedAt = DateTime.UtcNow.AddSeconds(1),
                        CreatedAt = DateTime.UtcNow,
                        ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                        Status = TransactionStatusEnum.Completed,
                        Title = $"Campaign {campaignModel.Title} deleted",
                        TransactionType = TransactionTypeEnum.DeleteCampaign,
                        UserModelId = user.Id
                    };
                    await _db.PaymentTransactions.AddAsync(paymentTrans);
                    await _db.SaveChangesAsync();
                }

                campaignModel.IsArchived = true;
                campaignModel.Budget = 0;
                campaignModel.Status = false;
                _db.Campaigns.Update(campaignModel);
                await _db.SaveChangesAsync();

                transaction.Commit();

                _eventSender.Send(new CampaignDeletedEvent
                {
                    CampaignId = campaignModel.Id,
                    Amount = campaignModel.Budget,
                    CampaignName = campaignModel.Title,
                    Name = user.Fullname,
                    UserId = user.Id
                });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                transaction.Rollback();
                throw new Exception("Invalid Data");
            }
        }
    }

    public async Task Edit(string id, Campaign campaign)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User no found");
        if (user.IsBlocked) throw new Exception("User is blocked");
        if (!user.IsSubscribed) throw new Exception("User is not subscribed");

        var campaignModel = await _db.Campaigns
        .Include(e => e.ImageData)
        .Where(e => e.ExternalId == id).SingleOrDefaultAsync();
        if (campaignModel == null) throw new Exception("Campaign does not exits");
        if (campaignModel.IsArchived) throw new Exception("Campaing deleted");
        var oldbudget = campaignModel.Budget;
        var oldEPM = campaignModel.EPM;
        var oldTitle = campaignModel.Title;
        var available = user.Available;

        using (var transaction = _db.Database.BeginTransaction())
        {
            try
            {
                // Validate Available balance    
                available.Value = available.Value + campaignModel.Budget - campaign.Budget;
                available.Etag = await Nanoid.Nanoid.GenerateAsync(size: 12);
                _db.Availables.Update(available);
                await _db.SaveChangesAsync();
                if (available.Value < 0) throw new Exception("Negative balance");

                // Verificar si hay que cambiar la imagen
                if (campaignModel.ImageData?.ExternalId != campaign.ImageBundleId)
                {
                    // Desvincular la nueva imagen
                    if (campaignModel.ImageDataModelId != null)
                    {
                        var oldImageData = _db.ImageDatas.Where(e => e.Id == campaignModel.ImageDataModelId).Single();
                        oldImageData.Bound = false;
                        _db.ImageDatas.Update(oldImageData);
                    }

                    // Fetchear la nueva imagen o asignar null
                    var imageData = await _db.ImageDatas.Where(e => e.ExternalId == campaign.ImageBundleId).SingleOrDefaultAsync();
                    campaignModel.ImageDataModelId = imageData?.Id;
                    if (imageData != null)
                    {
                        imageData.Bound = true;
                        _db.ImageDatas.Update(imageData);
                    }
                }

                // Edit Campaign
                campaignModel.Budget = campaign.Budget;
                campaignModel.LastUpdated = DateTime.UtcNow;
                campaignModel.Description = campaign.Description;
                campaignModel.EPM = campaign.EPM;
                campaignModel.Title = campaign.Title;
                campaignModel.Url = campaign.Url;
                _db.Campaigns.Update(campaignModel);
                await _db.SaveChangesAsync();

                if (oldbudget != campaignModel.Budget)
                {
                    // Create Payment Transaction
                    var paymentTrans = new PaymentTransaction
                    {
                        Id = Guid.NewGuid(),
                        Amount = campaign.Budget,
                        CampaignModelId = campaignModel.Id,
                        CompletedAt = DateTime.UtcNow.AddSeconds(1),
                        CreatedAt = DateTime.UtcNow,
                        ExternalId = await Nanoid.Nanoid.GenerateAsync(size: 12),
                        Status = TransactionStatusEnum.Completed,
                        Title = $"Campaign {campaign.Title} edited",
                        TransactionType = TransactionTypeEnum.EditCampaign,
                        UserModelId = user.Id
                    };
                    await _db.PaymentTransactions.AddAsync(paymentTrans);
                    await _db.SaveChangesAsync();
                }
                transaction.Commit();
                _eventSender.Send(new CampaignEditedEvent
                {
                    CampaignId = campaignModel.Id,
                    AmountNew = campaignModel.Budget,
                    CampaignNameNew = campaignModel.Title,
                    EPMNew = campaignModel.EPM,
                    UserId = user.Id,
                    Name = user.Fullname,
                    AmountOld = oldbudget,
                    EPMOld = oldEPM,
                    CampaignNameOld = oldTitle
                });
            }
            catch (System.Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex.Message);
                throw new Exception("Error editing campaing");
            }
        }

    }

    public async Task<IActionResult> Explore(int offset, int limit, long timestamp)
    {
        string cacheKey = $"campaigns_{offset}_{limit}_{timestamp}";
        string cacheKeyETag = $"etag_{offset}_{limit}_{timestamp}";

        // Comprueba el encabezado If-None-Match
        var requestHeaders = _httpContextAccessor.HttpContext.Request.Headers;

        if (requestHeaders.ContainsKey("If-None-Match") &&
            _cache.TryGetValue(cacheKeyETag, out string storedETag) &&
            requestHeaders["If-None-Match"] == storedETag)
        {
            // Devuelve un encabezado de respuesta 304 Not Modified
            return new StatusCodeResult((int)HttpStatusCode.NotModified);
        }

        // Comprueba si los datos están en caché
        if (_cache.TryGetValue(cacheKey, out List<CampaignCard> campaigns))
        {
            // Si los datos están en caché, los devuelve directamente
            return new OkObjectResult(campaigns);
        }

        // Los datos no están en caché, realiza la consulta en la base de datos
        if (timestamp == 0)
        {

            var items = await _db.Campaigns
            .Include(e => e.ImageData)
            .Include(e => e.User)
            .Where(e => e.Status)
            .OrderByDescending(c => c.LastUpdated)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            campaigns = items
            .Select(e => new CampaignCard
            {
                AutorName = e.User.Fullname,
                AutorImageUrl = e.User.ThumbnailImageUrl,
                Description = e.Description,
                EPM = e.EPM,
                Id = e.ExternalId,
                Title = e.Title,
                ImageBundle = e.ImageData?.ConvertToImageData(),
                LastModified = new DateTimeOffset(e.LastUpdated!.Value).ToUnixTimeMilliseconds()
            })
            .ToList();
        }
        else
        {
            var last_timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;

            var items = await _db.Campaigns
            .Include(e => e.ImageData)
            .Include(e => e.User)
            .Where(e => e.Status)
            .Where(e => e.LastUpdated <= last_timestamp)
            .Where(e => !e.IsArchived)
            .OrderByDescending(c => c.LastUpdated)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            campaigns = items
            .Select(e => new CampaignCard
            {
                AutorName = e.User.Fullname,
                AutorImageUrl = e.User.ThumbnailImageUrl,
                Description = e.Description,
                EPM = e.EPM,
                Id = e.ExternalId,
                Title = e.Title,
                ImageBundle = e.ImageData?.ConvertToImageData(),
                LastModified = new DateTimeOffset(e.LastUpdated!.Value).ToUnixTimeMilliseconds()
            })
            .ToList();
        }

        if (timestamp != 0)
        {
            // Agrega los datos a la caché
            _cache.Set(cacheKey, campaigns, TimeSpan.FromMinutes(30));

            string newETag = await Nanoid.Nanoid.GenerateAsync(size: 12);
            _httpContextAccessor.HttpContext.Response.Headers["ETag"] = newETag;
            _cache.Set(cacheKeyETag, newETag, TimeSpan.FromMinutes(30)); // Almacena el nuevo ETag en la caché
        }

        // Devuelve los datos obtenidos
        return new OkObjectResult(campaigns);
    }

    public async Task ReportAbuse(AbuseReport report)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User no found");

        var campaign = await _db.Campaigns
        .Include(e => e.ImageData)
        .Include(e => e.User)
        .Where(e => e.ExternalId == report.CampaignExternalId)
        .SingleAsync();

        var abuseReportModel = new AbuseReportModel
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            CampaignId = campaign.Id,
            Reason = report.Reason
        };

        _db.AbuseReports.Add(abuseReportModel);
        await _db.SaveChangesAsync();

        _eventSender.Send(new CampaignAbuseReportedEvent
        {
            OwnerUserId = campaign.User.Id,
            Amount = campaign.Budget,
            CampaignDescription = campaign.Description,
            CampaignId = campaign.Id,
            CampaignName = campaign.Title,
            EPM = campaign.EPM,
            ImageUrl = campaign.ImageData?.Compressed,
            OwnerName = campaign.User.Fullname,
            ReporterUserId = user.Id,
            ReporterName = user.Fullname
        });
    }


    public async Task<PaginationList<MyCampaign>> GetAll(int? page, int? cant, string? filter)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        PaginationList<MyCampaign> list = new PaginationList<MyCampaign>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var query = _db.Campaigns
        .Include(e => e.ImageData)
        .Where(e => e.UserModelId == userId)
        .Where(e => e.IsArchived == false);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.Title.ToLower().Contains(filter.ToLower()));
        }

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new MyCampaign
        {
            Budget = e.Budget,
            EPM = e.EPM,
            Id = e.ExternalId,
            ImageBundleId = e.ImageData != null ? e.ImageData.ExternalId : "",
            Title = e.Title,
            Url = e.Url,
            Status = e.Status,
            LastClick = e.LastClick,
            LastShared = e.LastShared
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }

    public async Task<CampaignDetail> GetDetails(string id)
    {
        if (string.IsNullOrEmpty(id)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        var campaign = await _db.Campaigns
        .Where(e => e.ExternalId == id).Select(e => new CampaignDetail
        {
            Budget = e.Budget,
            Description = e.Description,
            EPM = e.EPM,
            Id = e.ExternalId,
            Status = e.Status,
            Title = e.Title,
            Url = e.Url,
            LastClick = e.LastClick,
            LastShared = e.LastShared
        }).SingleOrDefaultAsync();

        if (campaign == null) throw new Exception("Campaign does not exits");

        var imageD = _db.Campaigns
        .Include(e => e.ImageData)
        .Where(e => e.ExternalId == id)
        .Select(e => e.ImageData).SingleOrDefault();

        campaign.ImageBundle = imageD?.ConvertToImageData();

        return campaign;
    }

    public async Task Publish(string campaignId, bool toStatus)
    {
        if (string.IsNullOrEmpty(campaignId)) throw new Exception("Invalid Campaign ID");
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User no found");
        if (user.IsBlocked) throw new Exception("User is blocked");
        if (!user.IsSubscribed) throw new Exception("User is not subscribed");

        var campaignModel = await _db.Campaigns.Where(e => e.ExternalId == campaignId).SingleOrDefaultAsync();
        if (campaignModel == null) throw new Exception("Campaign does not exits");
        if (campaignModel.IsArchived) throw new Exception("Campaing deleted");

        if (toStatus)
        {
            if (campaignModel.Budget <= (campaignModel.EPM / 1000)) throw new Exception("Insufficient budget");
        }

        if (toStatus)
        {
            _eventSender.Send(new CampaignPublishedEvent
            {
                CampaignId = campaignModel.Id,
                Amount = campaignModel.Budget,
                CampaignName = campaignModel.Title,
                Name = user.Fullname,
                UserId = user.Id
            });
        }
        else
        {
            _eventSender.Send(new CampaignUnPublishedEvent
            {
                CampaignId = campaignModel.Id,
                Amount = campaignModel.Budget,
                CampaignName = campaignModel.Title,
                Name = user.Fullname,
                UserId = user.Id
            });

        }

        campaignModel.Status = toStatus;
        _db.Campaigns.Update(campaignModel);
        await _db.SaveChangesAsync();
    }

    public async Task<PaginationList<MyCampaignWithImages>> GetAllWithImages(int? page, int? cant, string? filter)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var userId = await _db.Users.Where(e => e.FirebaseId == firebaseId).Select(e => e.Id).SingleOrDefaultAsync();
        if (userId == Guid.Empty) throw new Exception("User no found");

        PaginationList<MyCampaignWithImages> list = new PaginationList<MyCampaignWithImages>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var query = _db.Campaigns
        .Include(e => e.ImageData)
        .Where(e => e.UserModelId == userId)
        .Where(e => e.IsArchived == false);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(e => e.Title.ToLower().Contains(filter.ToLower()));
        }

        var counter = await query.CountAsync();

        list.Items = await query
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new MyCampaignWithImages
        {
            Budget = e.Budget,
            EPM = e.EPM,
            Id = e.ExternalId,
            ImageBundle = e.ImageData != null ? e.ImageData.ConvertToImageData() : null,
            Title = e.Title,
            Url = e.Url,
            Status = e.Status,
            LastClick = e.LastClick,
            LastShared = e.LastShared
        })
        .ToListAsync();
        list.Pagination.Page = page.Value!;
        list.Pagination.TotalPages = (int)Math.Ceiling((double)counter / (double)cant!.Value);
        list.Pagination.Cant = list.Items.Count;
        return list;
    }
}