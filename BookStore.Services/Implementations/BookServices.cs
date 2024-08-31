using BookStore.Common.DTOs.Product;
using BookStore.Common.Exceptions;
using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly ApplicationDbContext _context;

        public BookServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Add(Book book, CancellationToken cancellationToken = default)
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
                var categoryList = _context.Categories.Where(c => book.Categories.Select(cc => cc.Id).ToList().Contains(c.Id));

                var newBook = new Book()
                {
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Author = book.Author,
                    Translator = book.Translator,
                    Edition = book.Edition,
                    PublishYear = book.PublishYear,
                    PublishMonth = book.PublishMonth,
                    Pages = book.Pages,
                    Size = book.Size,
                    CoverType = book.CoverType,
                    Quantity = book.Quantity,
                    PublicationId = book.PublicationId,
                    Categories = new List<Category>(),
                    Images = new List<BookImage>()
                };

                foreach (var category in categoryList)
                {
                    newBook.Categories.Add(category);
                }

                foreach (var image in book.Images)
                {
                    image.Book = newBook;
                    newBook.Images.Add(image);
                }

                _context.Books.Add(newBook);

                await _context.SaveChangesAsync();

                return newBook;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public Task<bool> Delete(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(Book book, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Book> Get(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Book>> GetAll(BookFilterDTO bookFilter = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (bookFilter is null)
                {
                    return await _context.Books
                        .Include(b => b.Categories)
                        .OrderBy(b => b.Title).ToListAsync();
                }

                var books = _context.Books
                    .Include(b => b.Categories)
                    .AsQueryable();

                books = books.Where(b => (string.IsNullOrWhiteSpace(bookFilter.FilterText)
                || b.Title.Contains(bookFilter.FilterText)
                || b.Author.Contains(bookFilter.FilterText)
                || b.Translator.Contains(bookFilter.FilterText)
                || b.ISBN.Contains(bookFilter.FilterText)
                || b.Categories.Any(c => c.Name.Contains(bookFilter.FilterText))
                || b.Publication.Name.Contains(bookFilter.FilterText))
                && (bookFilter.FilterPublicationId == 0 || b.Publication.Id == bookFilter.FilterPublicationId)
                && (bookFilter.FilterCategoryId == 0 || b.Categories.Any(c => c.Id == bookFilter.FilterCategoryId)));

                return await books.OrderBy(b => b.Title).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }
    }
}
