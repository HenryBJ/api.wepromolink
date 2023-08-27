using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IUserService
{
    Task<bool> Exits(string email);
    Task<bool> IsBlocked();
    Task BlockUser(string firebaseId, string reason);
    Task<bool> IsSubscribed();
    Task Create(User user, bool isSubscribed = false, string firebaseId = "");
    Task<bool> SignUp(SignUpData data);
    Task SetFirebaseUid(String email, String Uid);
    Task UpdateSubscription(SubscriptionInfo subscriptionInfo, string CustomerId);
    Task Deposit(PaymentTransaction payment);
    Task <PaymentMethodData[]> GetPaymentMethods();
}