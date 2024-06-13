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

        public async Task<Category> Add(Category category, CancellationToken cancellationToken)
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

                await _context.SaveChangesAsync(cancellationToken);

                return category;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<bool> Delete(int Id, CancellationToken cancellationToken)
        {
            if (Id <= 0)
            {
                throw new InvalidOperationException("Category Id is not valid.");
            }

            try
            {
                var category = await _context.Categories.Where(c => c.Id == Id).FirstOrDefaultAsync(cancellationToken);

                if (category is not null)
                {
                    category.IsDeleted = true;

                    await _context.SaveChangesAsync(cancellationToken);

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

        public async Task<bool> Edit(Category category, CancellationToken cancellationToken)
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
                var dbCategory = await _context.Categories.Where(c => c.Id == category.Id).FirstOrDefaultAsync(cancellationToken);

                if (dbCategory is not null)
                {
                    dbCategory.Name = category.Name;
                    dbCategory.ParentId = category.ParentId;

                    await _context.SaveChangesAsync(cancellationToken);

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

        public async Task<Category> Get(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories.Where(c => c.Id == Id).FirstOrDefaultAsync(cancellationToken);

                return category;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<List<Category>> GetAll(CancellationToken cancellationToken, string filterText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filterText))
                {
                    var categories = await _context.Categories.ToListAsync(cancellationToken);

                    return categories.OrderBy(c => c.Name).ToList();
                }
                else
                {
                    var categories = await _context.Categories.Where(c => c.Name.Contains(filterText)).ToListAsync(cancellationToken);

                    return categories.OrderBy(c => c.Name).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }
    }
}
