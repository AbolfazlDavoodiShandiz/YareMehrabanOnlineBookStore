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
        Task<List<Category>> GetAll(CancellationToken cancellationToken, string filterText = null);
        Task<Category> Get(int Id, CancellationToken cancellationToken);
        Task<Category> Add(Category category, CancellationToken cancellationToken);
        Task<bool> Edit(Category category, CancellationToken cancellationToken);
        Task<bool> Delete(int Id, CancellationToken cancellationToken);
    }
}
