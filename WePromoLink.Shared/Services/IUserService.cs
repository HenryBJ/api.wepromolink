using WePromoLink.DTO;
using WePromoLink.Models;

namespace WePromoLink.Services;

public interface IUserService
{
    Task<string> GetExternalId();
    Task<bool> Exits(string email);
    Task<bool> IsBlocked();
    Task BlockUser(string firebaseId, string reason);
    Task<bool> IsSubscribed();
    Task<decimal> DepositFee();
    Task<int> GetLevel();
    Task<decimal> WithdrawFee();
    Task Create(User user, bool isSubscribed = false, string firebaseId = "");
    Task Upgrade(string custumerId, Guid SubscriptionId);
    Task<bool> SignUp(SignUpData data);
    Task SetUserExtraInfo(string email, string uid, string photourl);
    Task UpdateSubscription(SubscriptionInfo subscriptionInfo, string CustomerId);
    Task Deposit(PaymentTransaction payment);
    Task <PaymentMethodData[]> GetPaymentMethods();
}