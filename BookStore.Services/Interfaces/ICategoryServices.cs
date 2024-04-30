using BookStore.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetAll();
        Task<Category> Get(int Id);
        Task<Category> Add(Category category);
        Task<bool> Edit(Category category);
        Task<bool> Delete(int Id);
    }
}
