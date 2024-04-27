namespace Infrastructure.Cache.Base
{
    public interface ICache
    {
        void Set(string cacheKey, string value);
    }
}
