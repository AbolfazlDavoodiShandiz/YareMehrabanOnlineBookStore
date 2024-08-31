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
        public string PublishDate { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }

        public ICollection<BookImageViewModel> Images { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }

        public PublicationViewModel Publication { get; set; }
    }
}
