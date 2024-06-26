﻿using BookStore.Common.Enums;
using BookStore.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities.Product
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public int Edition { get; set; }
        public string PublishDate { get; set; }
        public int PrintNo { get; set; }
        public int Pages { get; set; }
        public string Size { get; set; }
        public string CoverType { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }

        public ICollection<Category> Categories { get; set; }

        public int PublicationId { get; set; }
        public Publication Publication { get; set; }
    }
}