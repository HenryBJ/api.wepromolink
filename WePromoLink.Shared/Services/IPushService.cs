using WePromoLink.DTO.PushNotification;

namespace WePromoLink.Services;

public interface IPushService
{
    Task<PushNotification> GetPushNotification();
    Task SetPushNotification(Guid UserId, Action<PushNotification> pushReducer);
    Task SetPushNotification(string firebaseId, Action<PushNotification> pushReducer);

}