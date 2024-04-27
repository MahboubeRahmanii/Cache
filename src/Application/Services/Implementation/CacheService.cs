using Application.Services.Contract;
using Domain;
using Infrastructure.Cache.InMemory;
using Infrastructure.Cache.Distributed;
using System.Text.Json;

namespace Application.Services.Implementation
{
    public class CacheService : ICacheService
    {
        private IRedisCache _redisCache;
        private IInMemoryCache _inMemoryCache;

        public CacheService(IRedisCache redisCache, IInMemoryCache inMemoryCache)
        {
            _redisCache = redisCache;
            _inMemoryCache = inMemoryCache;
        }

        public BookInfo GetDistributedCache(int id)
        {
            var distributedCacheResult = _redisCache.Get(id.ToString()).Result;

            if (!string.IsNullOrEmpty(distributedCacheResult))
                return JsonSerializer.Deserialize<BookInfo>(distributedCacheResult);

            return new BookInfo();
        }

        public BookInfo GetInMemoryCache(int id)
        {
            var inMemoryCacheResult = _inMemoryCache.Get(id.ToString());

            if (!string.IsNullOrEmpty(inMemoryCacheResult))
                return JsonSerializer.Deserialize<BookInfo>(inMemoryCacheResult);

            return new BookInfo();
        }

        public void SetDistributedCache(int id, string content)
        {
            _redisCache.Set(id.ToString(), content);
        }

        public void SetInMemoryCache(int id, string content)
        {
            _inMemoryCache.Set(id.ToString(), content);
        }
    }
}
