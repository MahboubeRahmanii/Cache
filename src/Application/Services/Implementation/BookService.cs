using Application.Services.Contract;
using Domain;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.Services.Implementation
{
    public class BookService : IBookService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string serviceURL = "https://get.taaghche.com/v2/book/";
        private ICacheService _cacheService;

        public BookService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<BookInfo> Get(int id)
        {
            var inMemoryCachedResult = _cacheService.GetInMemoryCache(id);
            if (inMemoryCachedResult.book != null)
            {
                return inMemoryCachedResult;
            }

            var distributedCachedResult = _cacheService.GetDistributedCache(id);
            if (distributedCachedResult.book != null)
            {
                return distributedCachedResult;
            }
            var ApiResult = await GetBookByApi(id);
            _cacheService.SetInMemoryCache(id, JsonSerializer.Serialize(ApiResult));
            _cacheService.SetDistributedCache(id, JsonSerializer.Serialize(ApiResult));
            return ApiResult;
        }

        private async Task<BookInfo> GetBookByApi(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<BookInfo>(serviceURL + id.ToString());

            return response ?? new BookInfo();
        }
    }
}
