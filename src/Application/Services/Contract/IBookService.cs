using Domain;

namespace Application.Services.Contract
{
    public interface IBookService
    {
        public Task<BookInfo> Get(int id);
    }
}
