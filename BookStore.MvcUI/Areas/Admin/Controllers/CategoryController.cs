using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Product;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class CategoryController : BaseMvcController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [NonAction]
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
                        Id = category.Id,
                        Name = category.Name
                    };
                }

                list.Add(categoryViewModel);
            }

            return list;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryViewModel>>> List()
        {
            var list = await GenerateCategoryViewModelList();

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var list = await GenerateCategoryViewModelList();

            ViewBag.CategoryList = new SelectList(list, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addCategoryViewModel);
            }

            var category = await _categoryServices.Add(new Category
            {
                Name = addCategoryViewModel.Name,
                ParentId = addCategoryViewModel.ParentId == 0 ? null : addCategoryViewModel.ParentId
            });

            if (category.Id > 0)
            {
                return RedirectToAction("List", "Category", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("AddCategoryError", "خطایی در ثبت دسته بندی رخ داده است.");

                return View(addCategoryViewModel);
            }
        }
    }
}
