using BookStore.Entities.Common;

namespace BookStore.Entities.Product
{
    public class BookImage : BaseEntity
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public bool IsMain { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
