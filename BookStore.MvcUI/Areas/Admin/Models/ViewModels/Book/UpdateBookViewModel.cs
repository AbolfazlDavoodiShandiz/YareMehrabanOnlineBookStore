using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Publication;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book
{
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public int Edition { get; set; }
        public string PublishDate { get; set; }
        public int PrintNo { get; set; }
        public int Pages { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }

        public List<string> Categories { get; set; }

        public int PublicationId { get; set; }
        public PublicationViewModel Publication { get; set; }
    }
}
