using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Product
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "ورود نام دسته بندی ضروری است.")]
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
