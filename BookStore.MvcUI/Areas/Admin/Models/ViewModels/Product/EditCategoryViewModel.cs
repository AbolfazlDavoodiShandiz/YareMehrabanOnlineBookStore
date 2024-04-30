namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Product
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
    }
}
