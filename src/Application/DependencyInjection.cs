using Application.Services.Contract;
using Application.Services.Implementation;
using Infrastructure.Cache.Distributed;
using Infrastructure.Cache.InMemory;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IInMemoryCache, InMemoryCache>();
            services.AddScoped<IRedisCache, RedisCache>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            return services;
        }
    }
}
