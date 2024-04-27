using Infrastructure.Cache.Base;

namespace Infrastructure.Cache.InMemory
{
    public interface IInMemoryCache : ICache
    {
        string Get(string cacheKey);
    }
}
