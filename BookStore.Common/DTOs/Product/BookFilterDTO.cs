namespace BookStore.Common.DTOs.Product
{
    public class BookFilterDTO
    {
        public string FilterText { get; set; }
        public int FilterCategoryId { get; set; }
        public int FilterPublicationId { get; set; }
    }
}
