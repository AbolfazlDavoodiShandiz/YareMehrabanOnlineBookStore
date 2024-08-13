using AutoMapper;
using BookStore.Common.Enums;
using BookStore.Entities.Product;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;
using BookStore.Services.Implementations;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class BookController : BaseMvcController
    {
        private readonly IBookServices _bookServices;
        private readonly IPublicationServices _publicationServices;
        private readonly IMapper _mapper;

        public BookController(IBookServices bookServices, IPublicationServices publicationServices, IMapper mapper)
        {
            _bookServices = bookServices;
            _publicationServices = publicationServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookViewModel>>> List()
        {
            return View();
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
