namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public CategoryViewModel Parent { get; set; }
    }
}
