using Microsoft.AspNetCore.Mvc;
using WePromoLink.DTO;

namespace WePromoLink.Services;

public interface INotificationService
{
    Task<PaginationList<Notification>> Get(int? page, int? cant);
    Task<NotificationDetail> GetDetails(string id);
    Task MarkAsRead(string id);
    Task Delete(string id);

}