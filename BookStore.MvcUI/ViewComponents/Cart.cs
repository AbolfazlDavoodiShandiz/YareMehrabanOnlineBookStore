using Microsoft.AspNetCore.Mvc;

namespace BookStore.MvcUI.ViewComponents
{
    public class Cart : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Cart");
        }
    }
}
