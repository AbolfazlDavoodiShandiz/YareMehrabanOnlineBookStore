using BookStore.Data;
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
        Task<List<Category>> GetAll(string filterText = null, CancellationToken cancellationToken = default);
        Task<Category> Get(int Id, CancellationToken cancellationToken = default);
        Task<Category> Add(Category category, CancellationToken cancellationToken = default);
        Task<bool> Edit(Category category, CancellationToken cancellationToken = default);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default);
    }
}
