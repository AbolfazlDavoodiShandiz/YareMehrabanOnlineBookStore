using BookStore.Common.Enums;
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
                        Id = category.Parent.Id,
                        Name = category.Parent.Name
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
        public async Task<IActionResult> Update(ProductActionType productActionType, int? Id = null)
        {
            var list = await GenerateCategoryViewModelList();

            ViewBag.CategoryList = new SelectList(list, "Id", "Name");

            UpdateCategoryViewModel updateCategoryViewModel = new UpdateCategoryViewModel();

            updateCategoryViewModel.ProductActionType = productActionType;

            if (productActionType == ProductActionType.Create && Id.HasValue)
            {
                productActionType = ProductActionType.Update;
                updateCategoryViewModel.ProductActionType = ProductActionType.Update;
            }

            if (productActionType == ProductActionType.Create)
            {
                return View(updateCategoryViewModel);
            }
            else if (productActionType == ProductActionType.Update)
            {
                var category = await _categoryServices.Get(Id.GetValueOrDefault());

                if (category is null)
                {
                    return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
                }

                updateCategoryViewModel.Id = category.Id;
                updateCategoryViewModel.Name = category.Name;
                updateCategoryViewModel.ParentId = category.ParentId;
            }

            return View(updateCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryViewModel updateCategoryViewModel)
        {
            var list = await GenerateCategoryViewModelList();

            ViewBag.CategoryList = new SelectList(list, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(updateCategoryViewModel);
            }

            if (updateCategoryViewModel.ProductActionType == ProductActionType.Create)
            {
                var category = await _categoryServices.Add(new Category
                {
                    Name = updateCategoryViewModel.Name,
                    ParentId = updateCategoryViewModel.ParentId == 0 ? null : updateCategoryViewModel.ParentId
                });

                if (category.Id > 0)
                {
                    return RedirectToAction("List", "Category", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("AddCategoryError", "خطایی در ثبت دسته بندی رخ داده است.");

                    return View(updateCategoryViewModel);
                }
            }
            else if (updateCategoryViewModel.ProductActionType == ProductActionType.Update)
            {
                if (updateCategoryViewModel.Id == updateCategoryViewModel.ParentId)
                {
                    ModelState.AddModelError("UpdateCategoryError", "دسته بندی نمیتواند زیر مجموعه خودش باشد.");

                    return View(updateCategoryViewModel);
                }

                var category = new Category()
                {
                    Id = updateCategoryViewModel.Id,
                    Name = updateCategoryViewModel.Name,
                    ParentId = updateCategoryViewModel.ParentId == 0 ? null : updateCategoryViewModel.ParentId
                };

                bool result = await _categoryServices.Edit(category);

                if (result)
                {
                    return RedirectToAction("List", "Category", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("UpdateCategoryError", "خطایی در ویرایش دسته بندی رخ داده است.");

                    return View(updateCategoryViewModel);
                }
            }

            return RedirectToAction("List", "Category", new { area = "Admin" });
        }
    }
}
