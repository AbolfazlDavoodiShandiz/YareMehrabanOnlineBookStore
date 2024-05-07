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
        Task<List<Publication>> GetAll();
        Task<Publication> Get(int Id);
        Task<Publication> Add(Publication publisher);
        Task<bool> Edit(Publication publisher);
        Task<bool> Delete(int Id);
    }
}
