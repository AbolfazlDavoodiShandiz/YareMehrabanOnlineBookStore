using BookStore.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IBookServices
    {
        Task<List<Book>> GetAll(CancellationToken cancellationToken = default);
        Task<Book> Get(int Id, CancellationToken cancellationToken = default);
        Task<Book> Add(Book book, CancellationToken cancellationToken = default);
        Task<bool> Edit(Book book, CancellationToken cancellationToken = default);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default);
    }
}
