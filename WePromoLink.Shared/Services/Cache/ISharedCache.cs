namespace WePromoLink.Services.Cache;

public interface IShareCache
{
    void Set<T>(string key, T value) where T:class;
    void Set<T>(string key, T value, object options) where T:class;
    void Set<T>(string key, T value, TimeSpan ttl) where T:class;
    T? Get<T>(string key) where T:class;
    bool TryGetValue<T>(string key, out T? value) where T:class;
    public bool Remove(string key);
}