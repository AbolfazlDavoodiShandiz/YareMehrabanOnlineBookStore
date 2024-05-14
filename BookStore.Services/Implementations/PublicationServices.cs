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

        public async Task<Publication> Add(Publication publication, CancellationToken cancellationToken)
        {
            if (publication is null)
            {
                throw new ArgumentNullException("Publication");
            }

            if (string.IsNullOrWhiteSpace(publication.Name))
            {
                throw new InvalidOperationException("Publication name can not be null.");
            }

            try
            {
                _context.Publications.Add(publication);

                await _context.SaveChangesAsync(cancellationToken);

                return publication;
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
                throw new InvalidOperationException("Publication Id is not valid.");
            }

            try
            {
                var publication = await _context.Publications.Where(c => c.Id == Id).FirstOrDefaultAsync(cancellationToken);

                if (publication is not null)
                {
                    publication.IsDeleted = true;

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

        public async Task<bool> Edit(Publication publication, CancellationToken cancellationToken)
        {
            if (publication.Id <= 0)
            {
                throw new InvalidOperationException("Publication Id is not valid.");
            }

            if (string.IsNullOrWhiteSpace(publication.Name))
            {
                throw new InvalidOperationException("Publication name can not be null.");
            }

            try
            {
                var dbPublication = await _context.Publications.Where(c => c.Id == publication.Id).FirstOrDefaultAsync(cancellationToken);

                if (dbPublication is not null)
                {
                    dbPublication.Name = publication.Name;
                    dbPublication.Address = publication.Address;
                    dbPublication.WebSiteUrl = publication.WebSiteUrl;

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

        public async Task<Publication> Get(int Id, CancellationToken cancellationToken)
        {
            try
            {
                var publication = await _context.Publications.Where(c => c.Id == Id).FirstOrDefaultAsync(cancellationToken);

                return publication;
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }

        public async Task<List<Publication>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var publications = await _context.Publications.ToListAsync(cancellationToken);

                return publications.OrderBy(c => c.Name).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationDatabaseOperationException(ex);
            }
        }
    }
}
