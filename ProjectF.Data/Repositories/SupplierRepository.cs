using ProjectF.Data.Context;
using ProjectF.Data.Entities.Suppliers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace ProjectF.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>
    {
        private readonly _AppDbContext _context;

        public SupplierRepository(_AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override Option<Supplier> Find(params object[] keyValues)
        {
            int id = parseInt(keyValues.FirstOrDefault()?.ToString() ?? "0").Match(c => c, () => 0);

            return _context.Suppliers
                .Include(s => s.Country)
                .Include(s => s.PaymentTerm)
                .FirstOrDefault(c => c.Id == id);
        }

        public override IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers
            .Include(s => s.Country)
            .Include(s => s.PaymentTerm);
        }
    }
}
