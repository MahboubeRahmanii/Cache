using Infrastructure.Cache.Base;

namespace Infrastructure.Cache.Distributed
{
    public interface IRedisCache : ICache
    {
        Task<string> Get(string cacheKey);
    }
}
