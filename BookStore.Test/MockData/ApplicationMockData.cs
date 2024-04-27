using BookStore.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Test.MockData
{
    internal class ApplicationMockData
    {
        internal List<Category> GenerateMockCategories()
        {
            return new List<Category>()
            {
                new Category{Id=1,Name="Literature"},
                new Category{Id=2,Name="History"},
                new Category{Id=3,Name="Astronomy"},
                new Category{Id=4,Name="Philosophy"},
                new Category{Id=5,Name="Geography"},
            };
        }
    }
}
