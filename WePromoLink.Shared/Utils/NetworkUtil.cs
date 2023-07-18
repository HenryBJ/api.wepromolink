using System.Net;
using System.Net.NetworkInformation;

namespace WePromoLink;

public static class NetworkUtil
{
    public static async Task<IPAddress?> GetRealLocalIP()
    {
        // Obtener la dirección IP real a través de una interfaz de red específica
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            var ipProperties = networkInterface.GetIPProperties();
            var unicastAddresses = ipProperties.UnicastAddresses;

            foreach (var unicastAddress in unicastAddresses)
            {
                if (unicastAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(unicastAddress.Address))
                {
                    return unicastAddress.Address;
                }
            }
        }
        return null;
    }
}