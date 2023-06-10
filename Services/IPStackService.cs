using Polly;
using WePromoLink.DTO.IPStack;
using WePromoLink.Models;

namespace WePromoLink.Services;

public class IPStackService
{
    private readonly string URL = "http://api.ipstack.com/{0}?access_key={1}";
    private readonly string _apiKey = "";

    public IPStackService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<GeoDataModel?> Locate(string ip)
    {
        var httpClient = new HttpClient();

        var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        GeoDataModel result = null;

        await retryPolicy.ExecuteAsync(async () =>
          {
              var requestUrl = string.Format(URL, ip, _apiKey);
              var response = await httpClient.GetAsync(requestUrl);
              response.EnsureSuccessStatusCode();
            //   var dataString = await response.Content.ReadAsStringAsync();
              var data = await response.Content.ReadFromJsonAsync<IPStackGeoData>();
              if (data == null) throw new Exception("Invalid GeoData");

              result = new GeoDataModel
              {
                  Id = Guid.NewGuid(),
                  City = data?.city,
                  Continent = data?.continent_name,
                  Country = data?.country_name,
                  CountryCode = data?.country_code,
                  CountryFlagUrl = data?.location?.country_flag,
                  Currency = data?.currency?.code,
                  IP = data?.ip ?? ip,
                  ISP = data?.connection?.isp,
                  Latitude = Convert.ToDecimal(data?.latitude),
                  Longitude = Convert.ToDecimal(data?.longitude),
                  Region = data?.region_name,
                  RegionCode = data?.region_code,
              };
          });
        return result;
    }
}