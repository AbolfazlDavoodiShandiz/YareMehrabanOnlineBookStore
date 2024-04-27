using BookStore.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Product
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
    }
}
