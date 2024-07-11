using BookStore.Common.Enums;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class BookController : BaseMvcController
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [HttpGet]
        public async Task<IActionResult> Update(CancellationToken cancellationToken, ProductActionType productActionType, int? Id = null)
        {
            UpdateBookViewModel updateBookViewModel = new UpdateBookViewModel();

            return View(updateBookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CancellationToken cancellationToken, UpdateBookViewModel updateBookViewModel)
        {
            if (!ModelState.IsValid)
            {
            }

            return View(updateBookViewModel);
        }
    }
}
