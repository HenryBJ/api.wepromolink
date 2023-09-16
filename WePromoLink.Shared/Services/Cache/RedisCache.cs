using Newtonsoft.Json;
using StackExchange.Redis;

namespace WePromoLink.Services.Cache;

public class RedisCache : IShareCache, IDisposable
{
    private readonly string _host;
    private readonly string _password;
    private readonly string _port;
    private IDatabase _db;
    private ConnectionMultiplexer? conn;

    public RedisCache(string host, string port, string password)
    {
        _host = host;
        _port = port;
        _password = password;
        Connect();
    }

    private void Connect()
    {
        conn = ConnectionMultiplexer.Connect($"{_host}:{_port},password={_password}");
        _db = conn.GetDatabase();
    }

    public void Set<T>(string key, T value) where T : class
    {
        _db.StringSet(key, JsonConvert.SerializeObject(value));
    }

    public void Set<T>(string key, T value, object options) where T : class
    {
        _db.StringSet(key, JsonConvert.SerializeObject(value));
    }

    public void Set<T>(string key, T value, TimeSpan ttl) where T : class
    {
        _db.StringSet(key, JsonConvert.SerializeObject(value), ttl);
    }

    public bool TryGetValue<T>(string key, out T? value) where T : class
    {
        if (_db.KeyExists(key))
        {
            string? stringValue = _db.StringGet(key);
            if (String.IsNullOrEmpty(stringValue))
            {
                value = default;
                return false;
            }
            value = JsonConvert.DeserializeObject<T>(stringValue);
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    public T? Get<T>(string key) where T : class
    {
        string? cad = _db.StringGet(key);
        if (cad != null)
        {
            return JsonConvert.DeserializeObject<T>(cad);
        }
        return default;
    }

    public void Dispose()
    {
        conn?.Close();
    }
}