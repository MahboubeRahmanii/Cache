using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Cache.Distributed
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<string> Get(string cacheKey)
        {
            var distributedCacheResult = await _distributedCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(distributedCacheResult))
                return string.Empty;

            return distributedCacheResult;
        }

        public async void Set(string cacheKey, string value)
        {
            await _distributedCache.SetStringAsync(cacheKey, value);
        }
    }
}
