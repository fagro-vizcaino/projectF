using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Context;
using ProjectF.Data.Entities;

namespace ProjectF.Data.Repositories
{
    public class CompanyRepository : _BaseRepository<Company>
    {
        readonly _AppDbContext _context;

        public CompanyRepository(_AppDbContext context) : base(context)
            => _context = context;
    }
}
