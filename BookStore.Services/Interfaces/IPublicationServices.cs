using BookStore.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IPublicationServices
    {
        Task<List<Publication>> GetAll(CancellationToken cancellationToken = default);
        Task<Publication> Get(int Id, CancellationToken cancellationToken = default);
        Task<Publication> Add(Publication publication, CancellationToken cancellationToken = default);
        Task<bool> Edit(Publication publication, CancellationToken cancellationToken = default);
        Task<bool> Delete(int Id, CancellationToken cancellationToken = default);
    }
}
