using BookStore.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Product
{
    public class Publication : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string WebSiteUrl { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
