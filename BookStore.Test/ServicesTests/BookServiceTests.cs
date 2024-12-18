using BookStore.Common.DTOs.Product;
using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using BookStore.Test.MockData;
using Moq;

namespace BookStore.Test.ServicesTests
{
    public class BookServiceTests
    {
        private readonly Mock<ApplicationDbContext> _context;
        private readonly IBookServices _services;

        public BookServiceTests()
        {
            _context = new ApplicationMockDbContext().GenerateMockDbContext();
            _services = new BookServices(_context.Object);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task GetAll_Should_Return_List_Of_Type_Book()
        {
            var result = await _services.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task GetAll_With_Filter_Should_Return_List_Of_Type_Book()
        {
            var result = await _services.GetAll(new BookFilterDTO
            {
                FilterText = "book"
            });

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Get_Should_Return_Null_If_Invalid_Id_Passed()
        {
            var result = await _services.Get(-1);

            Assert.Null(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Get_Should_Return_Of_Type_Book_If_Valid_Id_Passed()
        {
            var result = await _services.Get(1);

            Assert.IsType<Book>(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Add_Passed_Book_Should_Be_Not_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _services.Add(null, null));
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Add_Passed_Book_Title_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Book book = new Book()
            {
                Title = string.Empty,
                ISBN = "1234",
                Author = "Abolfazl",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Add(book, null));

            Assert.Equal("Book title should not be empty.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Add_Passed_Book_ISBN_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Book book = new Book()
            {
                Title = "Name",
                ISBN = string.Empty,
                Author = "Abolfazl",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Add(book, null));

            Assert.Equal("Book ISBN should not be empty.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Add_Passed_Book_Author_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Book book = new Book()
            {
                Title = "Name",
                ISBN = "1234",
                Author = string.Empty,
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Add(book, null));

            Assert.Equal("Book author should not be empty.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Add_Returned_Book_Id_Is_Greater_Than_Zero_If_Succeed()
        {
            Book book = new Book()
            {
                Id = 1,
                Title = "Name",
                ISBN = "1234",
                Author = "Author",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var result = await _services.Add(book, null);

            Assert.True(result.Id > 0);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Edit_Passed_Book_Id_Should_Be_Valid()
        {
            Book book = new Book()
            {
                Id = -1,
                Title = "Name",
                ISBN = "1234",
                Author = "Author",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Edit(book, null, null));

            Assert.Equal("Book Id is invalid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Edit_Return_False_If_Book_Not_Found()
        {
            Book book = new Book()
            {
                Id = 1000,
                Title = "Name",
                ISBN = "1234",
                Author = "Author",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var result = await _services.Edit(book, null, null);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Edit_Return_True_If_Succeed()
        {
            Book book = new Book()
            {
                Id = 1,
                Title = "Name",
                ISBN = "1234",
                Author = "Author",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var result = await _services.Edit(book, null, null);

            Assert.True(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Edit_Passed_Book_Title_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Book book = new Book()
            {
                Id = 1,
                Title = string.Empty,
                ISBN = "1234",
                Author = "Abolfazl",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Edit(book, null, null));

            Assert.Equal("Book title should not be empty.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Edit_Passed_Book_ISBN_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Book book = new Book()
            {
                Id = 1,
                Title = "Name",
                ISBN = string.Empty,
                Author = "Abolfazl",
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Edit(book, null, null));

            Assert.Equal("Book ISBN should not be empty.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Edit_Passed_Book_Author_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Book book = new Book()
            {
                Id = 1,
                Title = "Name",
                ISBN = "1234",
                Author = string.Empty,
                Translator = null,
                Edition = "5",
                PublishMonth = 6,
                PublishYear = 1403,
                Pages = 642,
                Size = "A5",
                CoverType = "HardCover",
                IsDeleted = false,
                Quantity = 100,
                Categories = new List<Category> { new Category { Id = 1 } },
                Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } }
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Edit(book, null, null));

            Assert.Equal("Book author should not be empty.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Delete_Passed_Book_Id_Should_Be_Greater_Than_Zero()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _services.Delete(0));

            Assert.Equal("Book Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Delete_Return_False_If_Book_Not_Found()
        {
            var result = await _services.Delete(1000);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task Delete_Return_True_If_Succeed()
        {
            var result = await _services.Delete(1);

            Assert.True(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task GetNewPublishedBooks_Should_Return_List_Of_Type_Book()
        {
            var result = await _services.GetNewPublishedBooks();

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        [Trait("Services", "Book")]
        public async Task GetNewPublishedBooks_Should_Return_List_Of_Books_Published_In_CurrentYear_Last6Month()
        {
            var result = await _services.GetNewPublishedBooks();

            int year = 2024;
            int month = 5;
            bool hasNewBook = false;

            foreach (var book in result)
            {
                if (book.PublishYear == 2024 && (month - book.PublishMonth) <= 6)
                {
                    hasNewBook = true;
                }
            }

            Assert.True(hasNewBook);
            Assert.IsType<List<Book>>(result);
        }
    }
}
