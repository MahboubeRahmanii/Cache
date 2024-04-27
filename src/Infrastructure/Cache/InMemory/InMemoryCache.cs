using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Cache.InMemory
{
    public class InMemoryCache : IInMemoryCache
    {
        private IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public string Get(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out string? content))
            {
                return content ?? string.Empty;
            }
            return string.Empty;
        }

        public void Set(string cacheKey, string value)
        {
            _memoryCache.Set(cacheKey, value);
        }
    }
}
