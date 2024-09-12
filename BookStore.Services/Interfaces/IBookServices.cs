using BookStore.Common.DTOs.Product;
using BookStore.Entities.Product;
using Microsoft.AspNetCore.Http;

namespace BookStore.Services.Interfaces
{
    public interface IBookServices
    {
        Task<List<Book>> GetAll(BookFilterDTO bookFilter = null, CancellationToken cancellationToken = default);
        Task<Book> Get(int Id, CancellationToken cancellationToken = default);
        Task<Book> Add(Book book, List<IFormFile> imageFilesToAdd, CancellationToken cancellationToken = default);
        Task<bool> Edit(Book book, List<IFormFile> imageFilesToAdd, string imageNamesToDelete,
            CancellationToken cancellationToken = default);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default);
    }
}
