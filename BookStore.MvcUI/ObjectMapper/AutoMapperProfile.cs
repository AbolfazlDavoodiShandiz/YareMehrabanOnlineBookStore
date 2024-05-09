using AutoMapper;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;

namespace BookStore.MvcUI.ObjectMapper
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
        }
    }
}
