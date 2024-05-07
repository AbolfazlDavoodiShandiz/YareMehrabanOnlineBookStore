using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category
{
    public class UpdateCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ورود نام دسته بندی ضروری است.")]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public ProductActionType ProductActionType { get; set; }
    }
}
