namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book
{
    public class BookListViewModel
    {
        public string FilterText { get; set; }
        public int FilterCategoryId { get; set; }
        public int FilterPublicationId { get; set; }

        public List<BookListItemViewModel> Books { get; set; }
    }
}
