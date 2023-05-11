using FirebaseAdmin.Auth;
using Microsoft.Extensions.Primitives;

namespace WePromoLink;

public static class FirebaseUtil
{

    public static async Task<UserRecord> GetUser(IHttpContextAccessor ca)
    {
        ca.HttpContext?.Request.Headers.TryGetValue("X-Wepromolink-UserId", out StringValues userId);
        var uId = userId[0];
        return await FirebaseAuth.DefaultInstance.GetUserAsync(uId);
    }

    public static string GetFirebaseId(IHttpContextAccessor ca)
    {
        ca.HttpContext?.Request.Headers.TryGetValue("X-Wepromolink-UserId", out StringValues userId);
        return userId[0];
    }
}