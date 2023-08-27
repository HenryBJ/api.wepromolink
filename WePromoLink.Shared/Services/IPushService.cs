using WePromoLink.DTO.PushNotification;

namespace WePromoLink.Services;

public interface IPushService
{
    Task<PushNotification> GetPushNotification();
    Task SetPushNotification(string firebaseId, PushNotification newPush);
    Task SetPushNotification(Guid UserId, PushNotification newPush);
    Task SetPushNotification(Guid UserId, Action<PushNotification> pushReducer);
    Task SetPushNotification(string firebaseId, Action<PushNotification> pushReducer);

    Task<PushNotification> UpdatePushNotification(PushNotification newNotification);
}