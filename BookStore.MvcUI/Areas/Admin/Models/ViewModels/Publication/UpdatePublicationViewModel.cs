using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication
{
    public class UpdatePublicationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ورود نام انتشارات ضروری است.")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string WebSiteUrl { get; set; }

        public ProductActionType ProductActionType { get; set; }
    }
}
