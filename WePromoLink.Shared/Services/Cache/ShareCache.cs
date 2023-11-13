using Microsoft.Extensions.Caching.Memory;

namespace WePromoLink.Services.Cache;

public class ShareCache : IShareCache
{
    private readonly IMemoryCache _memoryCache;

    public ShareCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T? Get<T>(string key) where T:class
    {
        return _memoryCache.Get(key) as T;
    }

    public bool Remove(string key)
    {
        _memoryCache.Remove(key);
        return true;
    }

    public void Set<T>(string key, T value) where T:class
    {
        _memoryCache.Set(key, value, new MemoryCacheEntryOptions { Size = 1 });
    }

    public void Set<T>(string key, T value, object options) where T:class
    {
        _memoryCache.Set(key, value, options as MemoryCacheEntryOptions);
    }

    public void Set<T>(string key, T value, TimeSpan ttl) where T : class
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue<T>(string key, out T? value) where T:class
    {
        return _memoryCache.TryGetValue(key, out value);
    }
}