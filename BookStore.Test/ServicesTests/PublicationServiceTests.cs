using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using BookStore.Test.MockData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Test.ServicesTests
{
    public class PublicationServiceTests
    {
        private readonly Mock<ApplicationDbContext> _context;
        private readonly IPublicationServices _publicationServices;
        private readonly CancellationToken _cancellationToken;

        public PublicationServiceTests()
        {
            _context = new ApplicationMockDbContext().GenerateMockDbContext();
            _publicationServices = new PublicationServices(_context.Object);
            _cancellationToken = new CancellationToken();
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Add_Passed_Publication_Should_Be_Not_Null()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _publicationServices.Add(null, _cancellationToken));
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Add_Passed_Publication_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Publication publication = new Publication()
            {
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _publicationServices.Add(publication, _cancellationToken));

            Assert.Equal("Publication name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Add_Returned_Publication_Id_Is_Greater_Than_Zero_If_Succeed()
        {
            Publication Publication = new Publication()
            {
                Id = 1,
                Name = "Boostan"
            };

            var result = await _publicationServices.Add(Publication, _cancellationToken);

            Assert.True(result.Id > 0);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task GetAll_Should_Return_List_Of_Type_Publication()
        {
            var result = await _publicationServices.GetAll(_cancellationToken);

            Assert.NotNull(result);
            Assert.IsType<List<Publication>>(result);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Get_Should_Return_Of_Type_Publication_If_Valid_Id_Passed()
        {
            var result = await _publicationServices.Get(1, _cancellationToken);

            Assert.NotNull(result);
            Assert.IsType<Publication>(result);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Get_Should_Return_Null_If_Invalid_Id_Passed()
        {
            var result = await _publicationServices.Get(0, _cancellationToken);

            Assert.Null(result);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Edit_Passed_Publication_Id_Should_Be_Greater_Than_Zero()
        {
            Publication publication = new Publication()
            {
                Id = 0,
                Name = "Boostan"
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _publicationServices.Edit(publication, _cancellationToken));

            Assert.Equal("Publication Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Edit_Passed_Publication_Name_Should_Be_Not_Null_Or_WhiteSpace()
        {
            Publication Publication = new Publication()
            {
                Id = 1,
                Name = string.Empty
            };

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _publicationServices.Edit(Publication, _cancellationToken));

            Assert.Equal("Publication name can not be null.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Edit_Return_False_If_Publication_Not_Found()
        {
            Publication Publication = new Publication()
            {
                Id = 1000000,
                Name = "Boostan"
            };

            var result = await _publicationServices.Edit(Publication, _cancellationToken);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Edit_Return_True_If_Succeed()
        {
            Publication Publication = new Publication()
            {
                Id = 1,
                Name = "Boostan"
            };

            var result = await _publicationServices.Edit(Publication, _cancellationToken);

            Assert.True(result);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Delete_Passed_Publication_Id_Should_Be_Greater_Than_Zero()
        {
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _publicationServices.Delete(0, _cancellationToken));

            Assert.Equal("Publication Id is not valid.", exception.Message);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Delete_Return_False_If_Publication_Not_Found()
        {
            var result = await _publicationServices.Delete(100000, _cancellationToken);

            Assert.False(result);
        }

        [Fact]
        [Trait("Services", "Publication")]
        public async Task Delete_Return_True_If_Succeed()
        {
            var result = await _publicationServices.Delete(1, _cancellationToken);

            Assert.True(result);
        }
    }
}
