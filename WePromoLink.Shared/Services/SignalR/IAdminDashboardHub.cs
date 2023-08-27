using WePromoLink.DTO.SignalR;

namespace WePromoLink.Services.SignalR;

public interface IAdminDashboardHub
{
    Task UpdateDashBoard(Action<DashboardStatus> updater);
}