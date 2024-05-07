using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication
{
    public class AddPublicationViewModel
    {
        [Required(ErrorMessage = "ورود نام انتشارات ضروری است.")]
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
