using ProjectF.Data.Context;
using ProjectF.Data.Entities.Clients;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;
using LanguageExt.Common;

namespace ProjectF.Data.Repositories
{
    public class ClientRepository : _BaseRepository<Client>
    {
        _AppDbContext _context;

        public ClientRepository(_AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Option<Client> FindByKey(long id)
            => _context.Clients
                .Include(c => c.Country)
                .FirstOrDefault(c => c.Id == id);
        

        public IEnumerable<Client> FindAll()
            => _context.Clients.Include(c => c.Country);

        public async Task<Either<Error, PagedList<Client>>> GetClientListAsync(ClientListParameters paramenters, bool trackChanges)
        {
            try
            {
                var clients = await FindByCondition(e => e.Id > 0, trackChanges)
                .Include(c => c.Country)
                .OrderBy(e => e.Id)
                .ToListAsync();

                return PagedList<Client>
                  .ToPagedList(clients, paramenters.PageNumber, paramenters.PageSize);
            }
            catch (Exception ex)
            {
                return Error.New(ex.Message);
            }
        }
    }
}
