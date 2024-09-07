using BookStore.Common.DTOs.Product;
using BookStore.Common.Exceptions;
using BookStore.Common.FilePaths;
using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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

        public async Task<Book> Add(Book book, IFormFileCollection formFiles, CancellationToken cancellationToken = default)
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
                var categoryList = _context.Categories
                    .Where(c => book.Categories.Select(cc => cc.Id).ToList()
                    .Contains(c.Id))
                    .ToList();

                book.Categories.Clear();

                foreach (var category in categoryList)
                {
                    book.Categories.Add(category);
                }

                bool imagesUploaded = await UploadImages(formFiles, book);

                if (!imagesUploaded)
                {
                    return null;
                }

                _context.Books.Add(book);

                await _context.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        private async Task<bool> UploadImages(IFormFileCollection formFiles, Book book,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (!Directory.Exists(ApplicationFilePath.BookPath))
                {
                    Directory.CreateDirectory(ApplicationFilePath.BookPath);
                }

                for (int i = 0; i < formFiles.Count; i++)
                {
                    string extension = Path.GetExtension(formFiles[i].FileName);
                    string originalName = formFiles[i].FileName;
                    string name = $"{Guid.NewGuid().ToString()}{extension}";
                    string filePath = $"{ApplicationFilePath.BookPath}/{name}";

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFiles[i].CopyToAsync(stream, cancellationToken);
                    }

                    BookImage image = new BookImage()
                    {
                        OriginalName = originalName,
                        Name = name,
                        IsMain = i == 0 ? true : false
                    };

                    book.Images.Add(image);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool DeleteImages(List<string> imageNames)
        {
            try
            {
                foreach (string imageName in imageNames)
                {
                    string filePath = $"{ApplicationFilePath.BookPath}/{imageName}";

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int Id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(Book book, IFormFileCollection formFiles, CancellationToken cancellationToken = default)
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (book.Id <= 0)
            {
                throw new InvalidOperationException("Book Id is invalid.");
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
                var bookToUpdate = await _context.Books
                    .Include(b => b.Categories)
                    .Include(b => b.Images)
                    .Where(b => b.Id == book.Id).SingleOrDefaultAsync();

                if (bookToUpdate is null)
                {
                    return false;
                }

                bookToUpdate.Title = book.Title;
                bookToUpdate.ISBN = book.ISBN;
                bookToUpdate.Author = book.Author;
                bookToUpdate.Translator = book.Translator;
                bookToUpdate.Edition = book.Edition;
                bookToUpdate.PublishYear = book.PublishYear;
                bookToUpdate.PublishMonth = book.PublishMonth;
                bookToUpdate.Pages = book.Pages;
                bookToUpdate.Size = book.Size;
                bookToUpdate.CoverType = book.CoverType;
                bookToUpdate.IsDeleted = book.IsDeleted;
                bookToUpdate.Quantity = book.Quantity;
                bookToUpdate.PublicationId = book.PublicationId;
                bookToUpdate.Categories.Clear();

                var imagesToDelete = bookToUpdate.Images.Select(x => x.Name).ToList();

                if (imagesToDelete.Count > 0)
                {
                    DeleteImages(imagesToDelete);
                    bookToUpdate.Images.Clear();
                }

                bool imagesUploaded = await UploadImages(formFiles, bookToUpdate);

                if (!imagesUploaded)
                {
                    return false;
                }

                var categoryList = _context.Categories
                    .Where(c => book.Categories.Select(cc => cc.Id).ToList()
                    .Contains(c.Id))
                    .ToList();

                foreach (var category in categoryList)
                {
                    bookToUpdate.Categories.Add(category);
                }

                _context.Books.Update(bookToUpdate);

                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<Book> Get(int Id, CancellationToken cancellationToken = default)
        {
            try
            {
                var book = await _context.Books
                    .Include(b => b.Categories)
                    .Include(b => b.Images)
                    .Where(b => b.Id == Id)
                    .FirstOrDefaultAsync(cancellationToken);

                return book;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<List<Book>> GetAll(BookFilterDTO bookFilter = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (bookFilter is null)
                {
                    return await _context.Books
                        .Include(b => b.Categories)
                        .Include(b => b.Images)
                        .OrderBy(b => b.Title).ToListAsync();
                }

                var books = _context.Books
                    .Include(b => b.Categories)
                    .Include(b => b.Images)
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

                return await books.OrderBy(b => b.Title).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }
    }
}
