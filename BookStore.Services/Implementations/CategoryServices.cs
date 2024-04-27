using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Implementations
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext _context;

        public CategoryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Add(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException("Category");
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new InvalidOperationException("Category name can not be null.");
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
