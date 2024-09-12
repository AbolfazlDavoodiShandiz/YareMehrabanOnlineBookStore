using BookStore.Common.FilePaths;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book
{
    public class BookImageViewModel
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public bool IsMain { get; set; }
        public int BookId { get; set; }
        public string Url
        {
            get
            {
                // add / in start of the path for client side
                string url = $"/{ApplicationFilePath.Book}/{Name}";

                // for client side remove wwwroot/ from path
                url = url.Replace($"{ApplicationFilePath.StaticFileRootFolder}/", "");

                return url;
            }
        }
    }
}
