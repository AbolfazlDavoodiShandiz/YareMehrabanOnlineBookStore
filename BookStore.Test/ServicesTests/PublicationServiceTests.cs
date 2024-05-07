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
    public class PublicationServiceTests
    {
        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Add_Passed_Publisher_Should_Be_Not_Null()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => publicationService.Add(null));
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Add_Passed_Publisher_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            Publication Publisher = new Publication()
            {
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => publicationService.Add(Publisher));

            Assert.Equal("Publisher name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Add_Returned_Publisher_Id_Is_Greater_Than_Zero_If_Succeed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            Publication Publisher = new Publication()
            {
                Id = 1,
                Name = "Boostan"
            };

            var result = await publicationService.Add(Publisher);

            Assert.True(result.Id > 0);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task GetAll_Should_Return_List_Of_Type_Publisher()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            var result = await publicationService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Publication>>(result);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Get_Should_Return_Of_Type_Publisher_If_Valid_Id_Passed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            var result = await publicationService.Get(1);

            Assert.NotNull(result);
            Assert.IsType<Publication>(result);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Get_Should_Return_Null_If_Invalid_Id_Passed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            var result = await publicationService.Get(0);

            Assert.Null(result);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Edit_Passed_Publisher_Id_Should_Be_Greater_Than_Zero()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            Publication Publisher = new Publication()
            {
                Id = 0,
                Name = "Boostan"
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => publicationService.Edit(Publisher));

            Assert.Equal("Publisher Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Edit_Passed_Publisher_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            Publication Publisher = new Publication()
            {
                Id = 1,
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => publicationService.Edit(Publisher));

            Assert.Equal("Publisher name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Edit_Return_False_If_Publisher_Not_Found()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            Publication Publisher = new Publication()
            {
                Id = 1000000,
                Name = "Boostan"
            };

            var result = await publicationService.Edit(Publisher);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Edit_Return_True_If_Succeed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            Publication Publisher = new Publication()
            {
                Id = 1,
                Name = "Boostan"
            };

            var result = await publicationService.Edit(Publisher);

            Assert.True(result);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Delete_Passed_Publisher_Id_Should_Be_Greater_Than_Zero()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => publicationService.Delete(0));

            Assert.Equal("Publisher Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Delete_Return_False_If_Publisher_Not_Found()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            var result = await publicationService.Delete(100000);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Publisher")]
        public async Task Delete_Return_True_If_Succeed()
        {
            var mockDbContext = new ApplicationMockDbContext().GenerateMockDbContext();

            var publicationService = new PublicationServices(mockDbContext.Object);

            var result = await publicationService.Delete(1);

            Assert.True(result);
        }
    }
}
