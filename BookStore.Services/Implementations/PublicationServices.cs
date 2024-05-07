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
    public class PublicationServices : IPublicationServices
    {
        private readonly ApplicationDbContext _context;

        public PublicationServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Publication> Add(Publication Publisher)
        {
            if (Publisher is null)
            {
                throw new ArgumentNullException("Publisher");
            }

            if (string.IsNullOrWhiteSpace(Publisher.Name))
            {
                throw new InvalidOperationException("Publisher name can not be null.");
            }

            try
            {
                _context.Publications.Add(Publisher);

                await _context.SaveChangesAsync();

                return Publisher;
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
                throw new InvalidOperationException("Publisher Id is not valid.");
            }

            try
            {
                var Publisher = await _context.Publications.Where(c => c.Id == Id).FirstOrDefaultAsync();

                if (Publisher is not null)
                {
                    _context.Publications.Remove(Publisher);

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

        public async Task<bool> Edit(Publication Publisher)
        {
            if (Publisher.Id <= 0)
            {
                throw new InvalidOperationException("Publisher Id is not valid.");
            }

            if (string.IsNullOrWhiteSpace(Publisher.Name))
            {
                throw new InvalidOperationException("Publisher name can not be null.");
            }

            try
            {
                var dbPublisher = await _context.Publications.Where(c => c.Id == Publisher.Id).FirstOrDefaultAsync();

                if (dbPublisher is not null)
                {
                    dbPublisher.Name = Publisher.Name;
                    dbPublisher.Address = Publisher.Address;
                    dbPublisher.WebSiteUrl = Publisher.WebSiteUrl;

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

        public async Task<Publication> Get(int Id)
        {
            try
            {
                var Publisher = await _context.Publications.Where(c => c.Id == Id).FirstOrDefaultAsync();

                return Publisher;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<List<Publication>> GetAll()
        {
            try
            {
                var Publishers = await _context.Publications.ToListAsync();

                return Publishers.OrderBy(c => c.Name).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }
    }
}
