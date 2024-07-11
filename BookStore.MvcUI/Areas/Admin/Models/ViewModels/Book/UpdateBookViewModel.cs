using BookStore.Common.Enums;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;
using BookStore.Services.Interfaces;
using Newtonsoft.Json;
using System.Text.Json;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public string Edition { get; set; }
        public string PublishYear { get; set; }
        public string PublishMonth { get; set; }
        public int Pages { get; set; }
        public int PrintNo { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }
        public ProductActionType ProductActionType { get; set; }

        public List<CategoryViewModel> Categories
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CategoriesString))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<List<CategoryViewModel>>(CategoriesString);
            }
        }
        public string CategoriesString { get; set; }

        public int PublicationId { get; set; }
        public PublicationViewModel Publication { get; set; }
    }
}
