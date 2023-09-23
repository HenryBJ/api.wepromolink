using Microsoft.EntityFrameworkCore;
using WePromoLink.Data;

namespace WePromoLink.Services.Profile;

public class ProfileService : IProfileService
{
    private readonly DataContext _db;
    public ProfileService(DataContext db)
    {
        _db = db;
    }
    public async Task<string> GetExternalId(string firebaseId)
    {
        string? result = await _db.Users
        .Where(e => e.FirebaseId == firebaseId)
        .Select(e => e.ExternalId)
        .SingleOrDefaultAsync();

        if(String.IsNullOrEmpty(result)) throw new Exception("firebaseId not found");

        return result;
    }
}