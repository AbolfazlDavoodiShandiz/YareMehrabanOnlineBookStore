using BookStore.Common.Exceptions;
using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly ApplicationDbContext _context;

        public BookServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Add(Book book)
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new InvalidOperationException("Book title should not be empty.");
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new InvalidOperationException("Book ISBN should not be empty.");
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new InvalidOperationException("Book author should not be empty.");
            }

            try
            {
                _context.Books.Add(book);

                await _context.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public Task<bool> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Book>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
