using Domain;

namespace Application.Services.Contract
{
    public interface ICacheService
    {
        public BookInfo GetInMemoryCache(int id);
        public BookInfo GetDistributedCache(int id);
        public void SetInMemoryCache(int id, string content);
        public void SetDistributedCache(int id, string content);
    }
}
