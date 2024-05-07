using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.MvcUI.ViewComponents
{
    public class MainMenu : ViewComponent
    {
        private readonly ICategoryServices _categoryServices;

        public MainMenu(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GenerateCategoryViewModelList();

            return View("MainMenu", categories);
        }

        private async Task<List<CategoryViewModel>> GenerateCategoryViewModelList()
        {
            var categories = await _categoryServices.GetAll();

            List<CategoryViewModel> list = new List<CategoryViewModel>();

            foreach (var category in categories)
            {
                var categoryViewModel = new CategoryViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId
                };

                if (category.ParentId is not null)
                {
                    categoryViewModel.Parent = new CategoryViewModel()
                    {
                        Id = category.Parent.Id,
                        Name = category.Parent.Name
                    };
                }

                list.Add(categoryViewModel);
            }

            return list;
        }
    }
}
