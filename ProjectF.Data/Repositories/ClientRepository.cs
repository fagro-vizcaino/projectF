using ProjectF.Data.Context;
using ProjectF.Data.Entities.Clients;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectF.Data.Repositories
{
    public class ClientRepository : _BaseRepository<Client>
    {
        Context._AppDbContext _context;

        public ClientRepository(Context._AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Option<Client> FindByKey(long id)
        {
            return _context.Clients
                .Include(c => c.Country)
                .FirstOrDefault(c => c.Id == id);
        }


        public IEnumerable<Client> FindAll()
        {
            return _context.Clients
                .Include(c => c.Country);
        }
    }
}
