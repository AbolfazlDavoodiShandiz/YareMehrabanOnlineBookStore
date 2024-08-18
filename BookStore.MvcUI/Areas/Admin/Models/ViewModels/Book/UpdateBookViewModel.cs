using BookStore.Common.Enums;
using BookStore.MvcUI.Areas.Admin.Models.ViewModels.Category;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BookStore.MvcUI.Areas.Admin.Models.ViewModels.Book
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "ورود عنوان کتاب ضروری است.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ورود شناسه کتاب ضروری است.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "فرمت شناسه صحیح نیست.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "ورود نویسنده کتاب ضروری است.")]
        public string Author { get; set; }

        public string Translator { get; set; }

        [Required(ErrorMessage = "ورود شماره نسخه کتاب ضروری است.")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "ورود سال انتشار کتاب ضروری است.")]
        public string PublishYear { get; set; }

        [Required(ErrorMessage = "ورود ماه انتشار کتاب ضروری است.")]
        public string PublishMonth { get; set; }

        [Required(ErrorMessage = "ورود تعداد صفحات کتاب ضروری است.")]
        [Range(1, int.MaxValue, ErrorMessage = "تعداد صفحات کتاب باید بزرگتر از یک باشد.")]
        public int Pages { get; set; }

        [Required(ErrorMessage = "ورود نوبت چاپ کتاب ضروری است.")]
        public int PrintNo { get; set; }

        [Required(ErrorMessage = "ورود سایز کتاب ضروری است.")]
        public string Size { get; set; }

        [Required(ErrorMessage = "ورود نوع جلد کتاب ضروری است.")]
        public string CoverType { get; set; }
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "ورود عنوان کتاب ضروری است.")]
        public int Quantity { get; set; }

        public ProductActionType ProductActionType { get; set; }

        [Required(ErrorMessage = "ورود دسته بندی کتاب ضروری است.")]
        public string CategoriesString { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "ورود انتشارات کتاب ضروری است.")]
        public int PublicationId { get; set; }

        public ICollection<BookImageViewModel> Images { get; set; } = new List<BookImageViewModel>();

        public List<CategoryViewModel> Categories
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CategoriesString))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<List<CategoryViewModel>>(CategoriesString);
            }
        }

        public string PublishDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PublishYear) || string.IsNullOrWhiteSpace(PublishMonth))
                {
                    return string.Empty;
                }

                return $"{PublishYear} {PublishMonth}";
            }
        }
    }
}
