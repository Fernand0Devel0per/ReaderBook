using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using ReaderBook.Core.Data.Caching.Interface;

namespace ReaderBook.Core.Data.Caching;

public class ChachingService : IChachingService
{
    private readonly IDistributedCache _cache;

    private readonly DistributedCacheEntryOptions _options;

    public ChachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
            SlidingExpiration = TimeSpan.FromHours(1),
        };
    }

    public async Task<string> GetAsync(string key)
        => await _cache.GetStringAsync(key);
 
    public async Task SetAsync(string key, string value)
        => await _cache.SetStringAsync(key, value, _options);

    public async Task RemoveAsync(string key)
        => await _cache.RemoveAsync(key);
}
