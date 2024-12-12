using BookStore.Common.Utility;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book
{
    public class BookListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public string Edition { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }
        public int PublishYear { get; set; }
        public int PublishMonth { get; set; }
        public string PublishMonthName
        {
            get
            {
                return DateUtility.GetPersianMonthName(PublishMonth);
            }
        }
        public string PublishDate
        {
            get
            {
                return $"{PublishMonthName} {PublishYear}";
            }
        }

        public string CategoriesString
        {
            get
            {
                if (Categories is null || Categories.Count == 0)
                {
                    return string.Empty;
                }

                return string.Join(" , ", Categories.Select(c => c.Name).ToList());
            }
        }

        public ICollection<BookImageViewModel> Images { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        public PublicationViewModel Publication { get; set; }
    }
}
