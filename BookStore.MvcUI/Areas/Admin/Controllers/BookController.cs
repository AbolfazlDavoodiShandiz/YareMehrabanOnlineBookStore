using AutoMapper;
using BookStore.Common.DTOs.Product;
using BookStore.Common.Enums;
using BookStore.Common.FilePaths;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book;
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
        public async Task<ActionResult<List<BookListViewModel>>> List(BookListViewModel bookListViewModel)
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
        public async Task<IActionResult> Update(CancellationToken cancellationToken, ProductActionType productActionType, int? Id = null)
        {
            UpdateBookViewModel updateBookViewModel = new UpdateBookViewModel();

            var publicationList = await _publicationServices.GetAll();

            ViewBag.PublicationsList = new SelectList(publicationList, "Id", "Name");

            return View(updateBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CancellationToken cancellationToken, UpdateBookViewModel updateBookViewModel)
        {
            var publicationList = await _publicationServices.GetAll();

            ViewBag.PublicationsList = new SelectList(publicationList, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View(updateBookViewModel);
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

                    if (!Directory.Exists(ApplicationFilePath.BookPath))
                    {
                        Directory.CreateDirectory(ApplicationFilePath.BookPath);
                    }

                    string extension = Path.GetExtension(Request.Form.Files[i].FileName);
                    string originalName = Request.Form.Files[i].FileName;
                    string name = $"{Guid.NewGuid().ToString()}{extension}";
                    string filePath = $"{ApplicationFilePath.BookPath}/{name}{extension}";

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await Request.Form.Files[i].CopyToAsync(stream);
                    }

                    BookImageViewModel image = new BookImageViewModel()
                    {
                        OriginalName = originalName,
                        Name = name,
                        IsMain = i == 0 ? true : false
                    };

                    updateBookViewModel.Images.Add(image);
                }

                var newBook = _mapper.Map<Book>(updateBookViewModel);

                var book = await _bookServices.Add(newBook);

                if (book.Id > 0)
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

            }

            return RedirectToAction("List", "Book", new { area = "Admin" });
        }
    }
}
