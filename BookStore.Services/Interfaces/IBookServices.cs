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
        Task<List<Book>> GetAll();
        Task<Book> Get(int Id);
        Task<Book> Add(Book book);
        Task<bool> Edit(Book book);
        Task<bool> Delete(int Id);
    }
}
