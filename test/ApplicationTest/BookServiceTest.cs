using Application.Services.Contract;
using Application.Services.Implementation;
using Domain;
using Moq;

namespace ApplicationTest
{
    [TestClass]
    public class BookServiceTest
    {
        [TestMethod]
        public async Task Get_ReturnsBookInfo_FromInMemoryCache_WhenAvailable()
        {
            int id = 3;
            var expectedBook = new Book { id = 3, price = 10, title = "Computer" };
            var expectedBookInfo = new BookInfo { book = expectedBook, shareText = "share" };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(c => c.GetInMemoryCache(id)).Returns(expectedBookInfo);

            var bookService = new BookService(cacheServiceMock.Object);

            var result = await bookService.Get(id);

            Assert.AreEqual(expectedBookInfo.shareText, result.shareText);
            Assert.AreEqual(expectedBookInfo.book.id, result.book?.id);
            Assert.AreEqual(expectedBookInfo.book.title, result.book?.title);
            Assert.AreEqual(expectedBookInfo.book.price, result.book?.price);
            cacheServiceMock.Verify(c => c.GetDistributedCache(id), Times.Never);
        }

        [TestMethod]
        public async Task Get_ReturnsBookInfo_FromDistributedCache_WhenNotInInMemoryCache()
        {
            int id = 3;
            var expectedBook = new Book { id = 3, price = 10, title = "Computer" };
            var expectedBookInfo = new BookInfo { book = expectedBook, shareText = "share" };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(c => c.GetInMemoryCache(id)).Returns(new BookInfo());
            cacheServiceMock.Setup(c => c.GetDistributedCache(id)).Returns(expectedBookInfo);

            var bookService = new BookService(cacheServiceMock.Object);

            var result = await bookService.Get(id);

            Assert.AreEqual(expectedBookInfo.shareText, result.shareText);
            Assert.AreEqual(expectedBookInfo.book.id, result.book?.id);
            Assert.AreEqual(expectedBookInfo.book.title, result.book?.title);
            Assert.AreEqual(expectedBookInfo.book.price, result.book?.price);
        }
    }
}
