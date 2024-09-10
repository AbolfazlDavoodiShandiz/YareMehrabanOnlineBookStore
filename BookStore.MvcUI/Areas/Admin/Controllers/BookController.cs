using AutoMapper;
using BookStore.Common.DTOs.Product;
using BookStore.Common.Enums;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class BookController : BaseMvcController
    {
        private readonly IBookServices _bookServices;
        private readonly IPublicationServices _publicationServices;
        private readonly ICategoryServices _categoryServices;
        private readonly IMapper _mapper;

        public BookController(IBookServices bookServices, IPublicationServices publicationServices, IMapper mapper, ICategoryServices categoryServices)
        {
            _bookServices = bookServices;
            _publicationServices = publicationServices;
            _mapper = mapper;
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookListViewModel>>> List(BookListViewModel bookListViewModel,
            CancellationToken cancellationToken = default)
        {
            var publicationList = await _publicationServices.GetAll();

            ViewBag.PublicationsList = new SelectList(publicationList, "Id", "Name");

            var categoryList = await _categoryServices.GetAll();

            ViewBag.CategoryList = new SelectList(categoryList, "Id", "Name");

            BookFilterDTO filterDTO = null;

            if (!string.IsNullOrWhiteSpace(bookListViewModel.FilterText)
                || bookListViewModel.FilterCategoryId != 0
                || bookListViewModel.FilterPublicationId != 0)
            {
                filterDTO = new BookFilterDTO
                {
                    FilterText = bookListViewModel.FilterText,
                    FilterCategoryId = bookListViewModel.FilterCategoryId,
                    FilterPublicationId = bookListViewModel.FilterPublicationId
                };
            }

            var books = await _bookServices.GetAll(filterDTO);

            bookListViewModel.Books = _mapper.Map<List<BookListItemViewModel>>(books);

            return View(bookListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(ProductActionType productActionType, int? Id = null,
            CancellationToken cancellationToken = default)
        {
            var publicationList = await _publicationServices.GetAll();

            ViewBag.PublicationsList = new SelectList(publicationList, "Id", "Name");

            var categories = await _categoryServices.GetAll(null, cancellationToken);

            ViewBag.CategoryList = _mapper.Map<List<CategoryViewModel>>(categories);

            if (productActionType == ProductActionType.Create)
            {
                return View(new UpdateBookViewModel { ProductActionType = productActionType });
            }
            else if (productActionType == ProductActionType.Update)
            {
                var book = await _bookServices.Get(Id.GetValueOrDefault(), cancellationToken);

                if (book is null)
                {
                    return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
                }

                var updateBookViewModel = _mapper.Map<UpdateBookViewModel>(book);
                updateBookViewModel.ProductActionType = productActionType;

                return View(updateBookViewModel);
            }
            else
            {
                return RedirectToAction("Error", "AdminHome", new { area = "Admin" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateBookViewModel updateBookViewModel,
            CancellationToken cancellationToken = default)
        {
            var publicationList = await _publicationServices.GetAll();

            ViewBag.PublicationsList = new SelectList(publicationList, "Id", "Name");

            var categories = await _categoryServices.GetAll(null, cancellationToken);

            ViewBag.CategoryList = _mapper.Map<List<CategoryViewModel>>(categories);

            if (!ModelState.IsValid)
            {
                return View(updateBookViewModel);
            }

            foreach (string categoryName in updateBookViewModel.CategoriesString.Split(";").ToList())
            {
                var category = categories.SingleOrDefault(c => c.Name == categoryName);

                updateBookViewModel.Categories.Add(new CategoryViewModel { Id = category.Id, Name = category.Name });
            }

            if (updateBookViewModel.ProductActionType == ProductActionType.Create)
            {
                List<string> allowedFileExtensions = new List<string>()
                {
                    "jpg",
                    "png"
                };

                for (int i = 0; i < Request.Form.Files.Count; i++)
                {
                    if (!allowedFileExtensions.Contains(Path.GetExtension(Request.Form.Files[i].FileName).Replace(".", "")))
                    {
                        ModelState.AddModelError("AddBookError", "فایل های انتخاب شده غیر مجاز است.");

                        return View(updateBookViewModel);
                    }
                }

                var newBook = _mapper.Map<Book>(updateBookViewModel);

                var book = await _bookServices.Add(newBook, Request.Form.Files, cancellationToken);

                if (book is not null && book.Id > 0)
                {
                    return RedirectToAction("List", "Book", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("AddBookError", "خطایی در ثبت کتاب رخ داده است.");

                    return View(updateBookViewModel);
                }
            }
            else if (updateBookViewModel.ProductActionType == ProductActionType.Update)
            {
                var book = _mapper.Map<Book>(updateBookViewModel);

                var updatedBook = await _bookServices.Edit(book, Request.Form.Files, cancellationToken);
            }

            return RedirectToAction("List", "Book", new { area = "Admin" });
        }
    }
}
