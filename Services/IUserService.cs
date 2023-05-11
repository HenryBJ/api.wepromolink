using WePromoLink.DTO;
namespace WePromoLink.Services;

public interface IUserService
{
    Task<bool> Exits(string email);
    Task<bool> IsBlocked();
    Task<bool> IsSubscribed();
    Task Create(User user, bool isSubscribed = false, string firebaseId = "");
    Task<bool> SignUp(SignUpData data);
    Task SetFirebaseUid(String email, String Uid);
    Task UpdateSubscription(SubscriptionInfo subscriptionInfo, string CustomerId);
}