using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Interfaces;
using WePromoLink.Models;

namespace WePromoLink.Services;

public class DataService : IDataService
{
    private readonly DataContext _db;
    private readonly ILogger<DataService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DataService(DataContext db, IHttpContextAccessor httpContextAccessor, ILogger<DataService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    

    public async Task<IActionResult> GetAvailable()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user != null) return new OkObjectResult(user.Available);
        return new NotFoundResult();
    }

    public async Task<IActionResult> GetBudget()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user != null) return new OkObjectResult(user.Budget);
        return new NotFoundResult();
    }

    
    public async Task<IActionResult> GetProfit()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).SingleOrDefaultAsync();
        if (user != null) return new OkObjectResult(user.Profit);
        return new NotFoundResult();
    }

}