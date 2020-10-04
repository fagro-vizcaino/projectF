using ProjectF.Data.Context;
using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;
using System;
using ProjectF.Data.Repositories;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Repositories
{
    public class TaxRegimeTypeRepository : _BaseRepository<TaxRegimeType>
    {
         readonly Context._AppDbContext _context;
        public TaxRegimeTypeRepository(Context._AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
