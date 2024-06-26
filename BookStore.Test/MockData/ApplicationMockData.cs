﻿using BookStore.Entities.Product;
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

        internal List<Publication> GenerateMockPublications()
        {
            return new List<Publication>()
            {
                new Publication{Id=1,Name="Sokhan",Address="Iran",WebSiteUrl="https://sokhanpub.net/"},
                new Publication{Id=2,Name="Jangal",Address="Iran",WebSiteUrl="https://jangal.com/"},
                new Publication{Id=3,Name="Packt",Address="USA",WebSiteUrl="https://www.packtpub.com/"},
                new Publication{Id=4,Name="O'Reilly",Address="USA",WebSiteUrl= "https://www.oreilly.com/"}
            };
        }

        internal List<Book> GenerateMockBooks()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Name",
                    ISBN = "1234",
                    Author = string.Empty,
                    Translator = null,
                    Edition = 5,
                    PublishDate = "May1994",
                    PrintNo = 12,
                    Pages = 642,
                    Size = "A5",
                    CoverType = "HardCover",
                    IsDeleted = false,
                    Quantity = 100
                },
                new Book()
                {
                    Id = 2,
                    Title = "Name",
                    ISBN = "1234",
                    Author = string.Empty,
                    Translator = null,
                    Edition = 5,
                    PublishDate = "May1994",
                    PrintNo = 12,
                    Pages = 642,
                    Size = "A5",
                    CoverType = "HardCover",
                    IsDeleted = false,
                    Quantity = 100
                }
            };
        }

    }
}
