using AutoMapper;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.MvcUI.ViewComponents
{
    public class MainMenu : ViewComponent
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public MainMenu(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var categories = await GenerateCategoryViewModelList(cancellationToken);

            return View("MainMenu", categories);
        }

        private async Task<List<CategoryViewModel>> GenerateCategoryViewModelList(CancellationToken cancellationToken)
        {
            var categories = await _categoryServices.GetAll(cancellationToken);

            var list = _mapper.Map<List<CategoryViewModel>>(categories);

            return list;
        }
    }
}
