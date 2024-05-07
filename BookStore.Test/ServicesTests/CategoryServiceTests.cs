using BookStore.Entities.Product;
using BookStore.Services.Implementations;
using BookStore.Test.MockData;
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
        [Fact]
        [Trait("Services", "Category")]
        public async Task Add_Passed_Category_Should_Be_Not_Null()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => categoryService.Add(null));
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Add_Passed_Category_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => categoryService.Add(category));

            Assert.Equal("Category name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Add_Returned_Category_Id_Is_Greater_Than_Zero_If_Succeed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Id = 1,
                Name = "Computer Science"
            };

            var result = await categoryService.Add(category);

            Assert.True(result.Id > 0);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task GetAll_Should_Return_List_Of_Type_Category()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            var result = await categoryService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Get_Should_Return_Of_Type_Category_If_Valid_Id_Passed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            var result = await categoryService.Get(1);

            Assert.NotNull(result);
            Assert.IsType<Category>(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Get_Should_Return_Null_If_Invalid_Id_Passed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            var result = await categoryService.Get(0);

            Assert.Null(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Passed_Category_Id_Should_Be_Greater_Than_Zero()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Id = 0,
                Name = "Computer Science"
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => categoryService.Edit(category));

            Assert.Equal("Category Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Passed_Category_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Id = 1,
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => categoryService.Edit(category));

            Assert.Equal("Category name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Return_False_If_Category_Not_Found()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Id = 1000000,
                Name = "Law"
            };

            var result = await categoryService.Edit(category);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Edit_Return_True_If_Succeed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Id = 1,
                Name = "Law"
            };

            var result = await categoryService.Edit(category);

            Assert.True(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Delete_Passed_Category_Id_Should_Be_Greater_Than_Zero()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => categoryService.Delete(0));

            Assert.Equal("Category Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Delete_Return_False_If_Category_Not_Found()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            var result = await categoryService.Delete(100000);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Category")]
        public async Task Delete_Return_True_If_Succeed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            var result = await categoryService.Delete(1);

            Assert.True(result);
        }
    }
}
