using BookStore.Common.Enums;
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
        public async Task<IActionResult> Update(ProductActionType productActionType, int? Id = null)
        {
            return View();
        }
    }
}
