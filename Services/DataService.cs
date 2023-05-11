using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stripe;
using WePromoLink.Data;
using WePromoLink.DTO;
using WePromoLink.Models;
using WePromoLink.Settings;

namespace WePromoLink.Services;

public class DataService : IDataService
{
    private readonly DataContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public DataService(DataContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AvailableModel> GetAvailable()
    {
        var firebaseId = FirebaseUtil.GetFirebaseId(_httpContextAccessor);
        var user = await _db.Users.Where(e => e.FirebaseId == firebaseId).Include(e => e.Available).SingleOrDefaultAsync();
        if (user != null) return user.Available;
        return null;
    }
}