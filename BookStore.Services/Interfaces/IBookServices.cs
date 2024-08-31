using BookStore.Common.DTOs.Product;
using BookStore.Entities.Product;

namespace BookStore.Services.Interfaces
{
    public interface IBookServices
    {
        Task<List<Book>> GetAll(BookFilterDTO bookFilter = null, CancellationToken cancellationToken = default);
        Task<Book> Get(int Id, CancellationToken cancellationToken = default);
        Task<Book> Add(Book book, CancellationToken cancellationToken = default);
        Task<bool> Edit(Book book, CancellationToken cancellationToken = default);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default);
    }
}
