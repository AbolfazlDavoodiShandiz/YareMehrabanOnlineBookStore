using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using BookStore.Test.MockData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Test.ServicesTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<ApplicationDbContext> _context;
        private readonly ICategoryServices _categoryServices;
        private readonly CancellationToken _cancellationToken;

        public CategoryServiceTests()
        {
            _context = new ApplicationMockDbContext().GenerateMockDbContext();
            _categoryServices = new CategoryServices(_context.Object);
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Add_Passed_Category_Should_Be_Not_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _categoryServices.Add(null, _cancellationToken));
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Add_Passed_Category_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Category category = new Category()
            {
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _categoryServices.Add(category, _cancellationToken));

            Assert.Equal("Category name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Add_Returned_Category_Id_Is_Greater_Than_Zero_If_Succeed()
        {
            Category category = new Category()
            {
                Id = 1,
                Name = "Computer Science"
            };

            var result = await _categoryServices.Add(category, _cancellationToken);

            Assert.True(result.Id > 0);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task GetAll_Should_Return_List_Of_Type_Category()
        {
            var result = await _categoryServices.GetAll(_cancellationToken);

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Get_Should_Return_Of_Type_Category_If_Valid_Id_Passed()
        {
            var result = await _categoryServices.Get(1, _cancellationToken);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Get_Should_Return_Null_If_Invalid_Id_Passed()
        {
            var result = await _categoryServices.Get(0, _cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Passed_Category_Id_Should_Be_Greater_Than_Zero()
        {
            Category category = new Category()
            {
                Id = 0,
                Name = "Computer Science"
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _categoryServices.Edit(category, _cancellationToken));

            Assert.Equal("Category Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Passed_Category_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Category category = new Category()
            {
                Id = 1,
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _categoryServices.Edit(category, _cancellationToken));

            Assert.Equal("Category name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Return_False_If_Category_Not_Found()
        {
            Category category = new Category()
            {
                Id = 1000000,
                Name = "Law"
            };

            var result = await _categoryServices.Edit(category, _cancellationToken);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Return_True_If_Succeed()
        {
            Category category = new Category()
            {
                Id = 1,
                Name = "Law"
            };

            var result = await _categoryServices.Edit(category, _cancellationToken);

            Assert.True(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Delete_Passed_Category_Id_Should_Be_Greater_Than_Zero()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _categoryServices.Delete(0, _cancellationToken));

            Assert.Equal("Category Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Delete_Return_False_If_Category_Not_Found()
        {
            var result = await _categoryServices.Delete(100000, _cancellationToken);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Delete_Return_True_If_Succeed()
        {
            var result = await _categoryServices.Delete(1, _cancellationToken);

            Assert.True(result);
        }
    }
}
