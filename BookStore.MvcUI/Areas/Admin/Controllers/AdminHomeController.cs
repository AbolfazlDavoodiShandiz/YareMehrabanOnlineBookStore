using Microsoft.AspNetCore.Mvc;

namespace BookStore.MvcUI.Areas.Admin.Controllers
{
    public class AdminHomeController : BaseMvcController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
