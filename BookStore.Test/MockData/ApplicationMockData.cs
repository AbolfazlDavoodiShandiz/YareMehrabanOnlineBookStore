﻿using BookStore.Entities.Product;

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
                    Translator = string.Empty,
                    Edition = "5",
                    PublishMonth = 6,
                    PublishYear=1403,
                    Pages = 642,
                    Size = "A5",
                    CoverType = "HardCover",
                    IsDeleted = false,
                    Quantity = 100,
                    Categories = GenerateMockCategories(),
                    Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } },
                    Publication=GenerateMockPublications().Single(p=>p.Id==1),
                    PublicationId = GenerateMockPublications().Single(p=>p.Id==1).Id
                },
                new Book()
                {
                    Id = 2,
                    Title = "Name",
                    ISBN = "1234",
                    Author = string.Empty,
                    Translator = string.Empty,
                    Edition = "5",
                    PublishMonth = 6,
                    PublishYear=1403,
                    Pages = 642,
                    Size = "A5",
                    CoverType = "HardCover",
                    IsDeleted = false,
                    Quantity = 100,
                    Categories = GenerateMockCategories(),
                    Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } },
                    Publication=GenerateMockPublications().Single(p=>p.Id==1),
                    PublicationId = GenerateMockPublications().Single(p=>p.Id==1).Id
                },
                new Book()
                {
                    Id = 2,
                    Title = "Name",
                    ISBN = "1234",
                    Author = string.Empty,
                    Translator = string.Empty,
                    Edition = "5",
                    PublishMonth = 1,
                    PublishYear=1403,
                    Pages = 642,
                    Size = "A5",
                    CoverType = "HardCover",
                    IsDeleted = false,
                    Quantity = 100,
                    Categories = GenerateMockCategories(),
                    Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } },
                    Publication=GenerateMockPublications().Single(p=>p.Id==1),
                    PublicationId = GenerateMockPublications().Single(p=>p.Id==1).Id
                },
                new Book()
                {
                    Id = 2,
                    Title = "Name",
                    ISBN = "1234",
                    Author = string.Empty,
                    Translator = string.Empty,
                    Edition = "5",
                    PublishMonth = 1,
                    PublishYear=1403,
                    Pages = 642,
                    Size = "A5",
                    CoverType = "HardCover",
                    IsDeleted = false,
                    Quantity = 100,
                    Categories = GenerateMockCategories(),
                    Images = new List<BookImage> { new BookImage { Name = "ImageName", OriginalName = "ImageOriginalName" } },
                    Publication=GenerateMockPublications().Single(p=>p.Id==1),
                    PublicationId = GenerateMockPublications().Single(p=>p.Id==1).Id
                }
            };
        }

    }
}
