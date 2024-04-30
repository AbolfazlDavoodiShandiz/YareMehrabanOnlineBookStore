using BookStore.Common.Exceptions;
using BookStore.Data;
using BookStore.Entities.Product;
using BookStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<bool> Delete(int Id)
        {
            if (Id <= 0)
            {
                throw new InvalidOperationException("Category Id is not valid.");
            }

            try
            {
                var category = await _context.Categories.Where(c => c.Id == Id).FirstOrDefaultAsync();

                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<bool> Edit(Category category)
        {
            if (category.Id <= 0)
            {
                throw new InvalidOperationException("Category Id is not valid.");
            }

            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new InvalidOperationException("Category name can not be null.");
            }

            try
            {
                var dbCategory = await _context.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync();

                if (dbCategory != null)
                {
                    dbCategory.Name = category.Name;
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<Category> Get(int Id)
        {
            try
            {
                var category = await _context.Categories.Where(c => c.Id == Id).FirstOrDefaultAsync();

                return category;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<List<Category>> GetAll()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                return categories;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }
    }
}
