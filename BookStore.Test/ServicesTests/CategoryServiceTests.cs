using BookStore.Entities.Product;
using BookStore.Services.Implementations;
using BookStore.Test.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task Add_Returned_Category_Id_Is_Greater_Than_Zero()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var categoryService = new CategoryServices(mockDbContext.Object);

            Category category = new Category()
            {
                Id=1,
                Name = "Computer Science"
            };

            var result = await categoryService.Add(category);

            Assert.True(result.Id > 0);
        }
    }
}
