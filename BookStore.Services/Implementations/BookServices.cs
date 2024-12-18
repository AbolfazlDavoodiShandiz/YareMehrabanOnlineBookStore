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

        public async Task<Book> Add(Book book, List<IFormFile> imageFilesToAdd, CancellationToken cancellationToken = default)
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

                bool imagesUploaded = await UploadImages(imageFilesToAdd, book);

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

        private async Task<bool> UploadImages(List<IFormFile> imageFiles, Book book,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (imageFiles != null)
                {
                    if (!Directory.Exists(ApplicationFilePath.Book))
                    {
                        Directory.CreateDirectory(ApplicationFilePath.Book);
                    }

                    for (int i = 0; i < imageFiles.Count; i++)
                    {
                        string extension = Path.GetExtension(imageFiles[i].FileName);
                        string originalName = imageFiles[i].FileName;
                        string name = $"{Guid.NewGuid().ToString()}{extension}";
                        string filePath = $"{ApplicationFilePath.Book}/{name}";

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await imageFiles[i].CopyToAsync(stream, cancellationToken);
                        }

                        BookImage image = new BookImage()
                        {
                            OriginalName = originalName,
                            Name = name,
                            IsMain = i == 0 ? true : false
                        };

                        book.Images.Add(image);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool DeleteImages(List<string> imageNames, Book book)
        {
            try
            {
                foreach (string imageName in imageNames)
                {
                    string filePath = $"{ApplicationFilePath.Book}/{imageName}";

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);

                        book.Images.Remove(book.Images.Single(x => x.Name == imageName));
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

        public async Task<bool> Edit(Book book, List<IFormFile> imageFilesToAdd, string imageNamesToDelete, CancellationToken cancellationToken = default)
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

                bool imageDeleted = false;

                if (!string.IsNullOrWhiteSpace(imageNamesToDelete))
                {
                    var imageNamesToDeleteArray = imageNamesToDelete.Split(';').ToList();

                    imageDeleted = DeleteImages(imageNamesToDeleteArray, bookToUpdate);

                    if (!imageDeleted)
                    {
                        return false;
                    }
                }

                bool imagesUploaded = await UploadImages(imageFilesToAdd, bookToUpdate);

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

        public async Task<List<Book>> GetNewPublishedBooks(CancellationToken cancellationToken = default)
        {
            List<Book> books = new List<Book>();



            return books;
        }
    }
}
