using AutoMapper;
using BookStore.Common.Enums;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class CategoryController : BaseMvcController
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryServices categoryServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryViewModel>>> List(CancellationToken cancellationToken)
        {
            var categories = await _categoryServices.GetAll(cancellationToken: cancellationToken);
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            return View(categoriesViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(CancellationToken cancellationToken, ProductActionType productActionType, int? Id = null)
        {
            var categories = await _categoryServices.GetAll(cancellationToken: cancellationToken);
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            ViewBag.CategoryList = new SelectList(categoriesViewModel, "Id", "Name");

            if (productActionType == ProductActionType.Create && Id.HasValue)
            {
                productActionType = ProductActionType.Update;
            }

            if (productActionType == ProductActionType.Create)
            {
                return View(new UpdateCategoryViewModel() { ProductActionType = productActionType });
            }
            else if (productActionType == ProductActionType.Update)
            {
                var category = await _categoryServices.Get(Id.GetValueOrDefault(), cancellationToken);

                if (category is null)
                {
                    return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
                }

                var updateCategoryViewModel = _mapper.Map<UpdateCategoryViewModel>(category);
                updateCategoryViewModel.ProductActionType = productActionType;

                return View(updateCategoryViewModel);
            }
            else
            {
                return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryViewModel updateCategoryViewModel, CancellationToken cancellationToken)
        {
            var categories = await _categoryServices.GetAll(cancellationToken: cancellationToken);
            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            ViewBag.CategoryList = new SelectList(categoriesViewModel, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(updateCategoryViewModel);
            }

            if (updateCategoryViewModel.ProductActionType == ProductActionType.Create)
            {
                var newCategory = _mapper.Map<Category>(updateCategoryViewModel);

                var category = await _categoryServices.Add(newCategory, cancellationToken);

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

                var updateCategory = _mapper.Map<Category>(updateCategoryViewModel);

                bool result = await _categoryServices.Edit(updateCategory, cancellationToken);

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

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var result = await _categoryServices.Delete(Id, cancellationToken);

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsJson(string filterText, CancellationToken cancellationToken)
        {
            var categories = await _categoryServices.GetAll(filterText, cancellationToken);

            var list = _mapper.Map<List<CategoryViewModel>>(categories);

            return Json(list);
        }
    }
}
