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

namespace WePromoLink.Services;

public class TransactionService : ITransactionService
{
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<TransactionService> _logger;
    public TransactionService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<TransactionService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<PaginationList<Transaction>> Get(int? page, int? cant)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");

        PaginationList<Transaction> list = new PaginationList<Transaction>();
        page = page ?? 1;
        page = page <= 0 ? 1 : page;
        cant = cant ?? 25;

        var counter = await _db.PaymentTransactions
        .Where(e => e.UserModelId == user.Id)
        .CountAsync();

        list.Items = await _db.PaymentTransactions
        .Where(e => e.UserModelId == user.Id)
        .OrderByDescending(e => e.CreatedAt)
        .Skip((page.Value! - 1) * cant!.Value)
        .Take(cant!.Value)
        .Select(e => new Transaction
        {
            Id = e.ExternalId,
            Amount = e.Amount,
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

    public async Task<TransactionDetail> GetDetails(string id)
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user == null) throw new Exception("User does not exits");

        var result = await _db.PaymentTransactions
        .Include(e => e.Campaign)
        .Include(e => e.Link)
        .Where(e => e.ExternalId == id)
        .Select(e => new TransactionDetail
        {
            Id = e.ExternalId,
            Amount = e.Amount,
            CampaignImageUrl = e.Campaign != null ? e.Campaign.ImageUrl : null,
            CampaignName = e.Campaign != null ? e.Campaign.Title : null,
            CompletedAt = e.CompletedAt,
            CreatedAt = e.CreatedAt,
            ExpiredAt = e.ExpiredAt,
            LinkImageUrl = e.Link != null ? e.Link.Url : null,
            Status = e.Status,
            Title = e.Title,
            TransactionType = e.TransactionType
        })
        .SingleOrDefaultAsync();
        if (result == null) throw new Exception("Payment Transaction no found");

        return result;
    }


}