using BookStore.Entities.Common;

namespace BookStore.Entities.Product
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public string Edition { get; set; }
        public int PublishYear { get; set; }
        public int PublishMonth { get; set; }
        public int Pages { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }

        public ICollection<BookImage> Images { get; set; }

        public ICollection<Category> Categories { get; set; }

        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}