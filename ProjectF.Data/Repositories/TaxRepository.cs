using ProjectF.Data.Context;
using ProjectF.Data.Entities.Taxes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Repositories
{
    public class TaxRepository : _BaseRepository<Tax>
    {
        readonly Context._AppDbContext _context;

        public TaxRepository(Context._AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
