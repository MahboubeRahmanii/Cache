using Application.Services.Implementation;
using Domain;
using Infrastructure.Cache.Distributed;
using Infrastructure.Cache.InMemory;
using Moq;
using System.Text.Json;

namespace ApplicationTest
{
    [TestClass]
    public class CacheServiceTest
    {
        [TestMethod]
        public void GetDistributedCache_CacheContainsValue_ReturnsDeserializedBookInfo()
        {
            var expectedBook = new Book { id = 3, price = 10, title = "Computer" };
            var expectedBookInfo = new BookInfo { book = expectedBook, shareText = "share" };

            var distributedCacheMock = new Mock<IRedisCache>();

            distributedCacheMock.Setup(c => c.Get(It.IsAny<string>()).Result)
                            .Returns(JsonSerializer.Serialize(expectedBookInfo));

            var cacheService = new CacheService(distributedCacheMock.Object, null);

            var result = cacheService.GetDistributedCache(3);

            Assert.AreEqual(expectedBookInfo.shareText, result.shareText);
            Assert.AreEqual(expectedBookInfo.book.id, result.book?.id);
            Assert.AreEqual(expectedBookInfo.book.title, result.book?.title);
            Assert.AreEqual(expectedBookInfo.book.price, result.book?.price);
        }

        [TestMethod]
        public void GetDistributedCache_CacheDoesNotContainValue_ReturnsNewBookInfo()
        {
            var distributedCacheMock = new Mock<IRedisCache>();

            distributedCacheMock.Setup(c => c.Get(It.IsAny<string>()).Result)
                            .Returns(string.Empty);

            var cacheService = new CacheService(distributedCacheMock.Object, null);

            var result = cacheService.GetDistributedCache(3);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetInMemoryCache_CacheContainsValue_ReturnsDeserializedBookInfo()
        {
            var expectedBook = new Book { id = 3, price = 10, title = "Computer" };
            var expectedBookInfo = new BookInfo { book = expectedBook, shareText = "share" };

            var inMemoryCacheMock = new Mock<IInMemoryCache>();

            inMemoryCacheMock.Setup(c => c.Get(It.IsAny<string>()))
                            .Returns(JsonSerializer.Serialize(expectedBookInfo));

            var cacheService = new CacheService(null, inMemoryCacheMock.Object);

            var result = cacheService.GetInMemoryCache(3);

            Assert.AreEqual(expectedBookInfo.shareText, result.shareText);
            Assert.AreEqual(expectedBookInfo.book.id, result.book?.id);
            Assert.AreEqual(expectedBookInfo.book.title, result.book?.title);
            Assert.AreEqual(expectedBookInfo.book.price, result.book?.price);
        }

        [TestMethod]
        public void GetInMemoryCache_CacheDoesNotContainValue_ReturnsNewBookInfo()
        {
            var inMemoryCacheMock = new Mock<IInMemoryCache>();

            inMemoryCacheMock.Setup(c => c.Get(It.IsAny<string>()))
                            .Returns(string.Empty);

            var cacheService = new CacheService(null, inMemoryCacheMock.Object);

            var result = cacheService.GetInMemoryCache(3);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SetDistributedCache_CallsRedisCacheSet_WithCorrectArguments()
        {
            int id = 123;
            string content = "test content";

            var redisCacheMock = new Mock<IRedisCache>();
            var cacheService = new CacheService(redisCacheMock.Object, null);

            cacheService.SetDistributedCache(id, content);

            redisCacheMock.Verify(c => c.Set(id.ToString(), content), Times.Once);
        }

        [TestMethod]
        public void SetInMemoryCache_CallsInMemoryCacheSet_WithCorrectArguments()
        {
            int id = 123;
            string content = "test content";

            var inMemoryCacheMock = new Mock<IInMemoryCache>();
            var cacheService = new CacheService(null, inMemoryCacheMock.Object);

            cacheService.SetInMemoryCache(id, content);

            inMemoryCacheMock.Verify(c => c.Set(id.ToString(), content), Times.Once);
        }
    }
}