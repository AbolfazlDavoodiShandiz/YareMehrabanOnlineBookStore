using BookStore.Common.DTOs.Product;
using BookStore.Entities.Product;
using Microsoft.AspNetCore.Http;

namespace BookStore.Services.Interfaces
{
    public interface IBookServices
    {
        Task<List<Book>> GetAll(BookFilterDTO bookFilter = null, CancellationToken cancellationToken = default);
        Task<Book> Get(int Id, CancellationToken cancellationToken = default);
        Task<Book> Add(Book book, IFormFileCollection formFile, CancellationToken cancellationToken = default);
        Task<bool> Edit(Book book, IFormFileCollection formFiles, CancellationToken cancellationToken = default);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default);
    }
}
