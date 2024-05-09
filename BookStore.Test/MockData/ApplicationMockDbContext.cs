using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Test.MockData
{
    internal class ApplicationMockDbContext
    {
        internal Mock<ApplicationDbContext> GenerateMockDbContext()
        {
            ApplicationMockData applicationMockData = new ApplicationMockData();

            var categories = applicationMockData.GenerateMockCategories();
            var publications = applicationMockData.GenerateMockPublications();
            var books = applicationMockData.GenerateMockBooks();

            var dbContextOptions = new DbContextOptionsBuilder();
            var dbContext = new Mock<ApplicationDbContext>(dbContextOptions.Options);

            dbContext.Setup(c => c.Categories).ReturnsDbSet(categories);
            dbContext.Setup(c => c.Publications).ReturnsDbSet(publications);
            dbContext.Setup(c => c.Books).ReturnsDbSet(books);

            return dbContext;
        }
    }
}
