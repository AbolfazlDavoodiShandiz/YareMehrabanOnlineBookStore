using AutoMapper;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;

namespace BookStore.MvcUI.Utility.ObjectMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();

            CreateMap<Category, UpdateCategoryViewModel>()
                .ForMember(d => d.ParentId, o => o.MapFrom(s => s.ParentId == 0 ? null : s.ParentId));
            CreateMap<UpdateCategoryViewModel, Category>()
                .ForMember(d => d.ParentId, o => o.MapFrom(s => s.ParentId == 0 ? null : s.ParentId));

            CreateMap<Publication, PublicationViewModel>();
            CreateMap<PublicationViewModel, Publication>();

            CreateMap<Publication, UpdatePublicationViewModel>();
            CreateMap<UpdatePublicationViewModel, Publication>();

            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();

            CreateMap<Book, UpdateBookViewModel>();
            CreateMap<UpdateBookViewModel, Book>();

            CreateMap<BookImage, BookImageViewModel>();
            CreateMap<BookImageViewModel, BookImage>();

            CreateMap<Book, BookListItemViewModel>();
            CreateMap<BookListItemViewModel, Book>();
        }
    }
}
