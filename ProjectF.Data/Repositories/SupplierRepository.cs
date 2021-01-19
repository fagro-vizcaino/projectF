using ProjectF.Data.Context;
using ProjectF.Data.Entities.Suppliers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF.Data.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>
    {
        private readonly Context._AppDbContext _context;

        public SupplierRepository(Context._AppDbContext context) : base(context)
        {
            _context = context;
        }


        public Supplier GetByKeys(long id)
        {
            return _context.Suppliers
                .Include(s => s.Country)
                .FirstOrDefault(c => c.Id == id);
        }


        public IEnumerable<Supplier> FindAll()
        {
                return _context.Suppliers
                .Include(s => s.Country);
        }
    }
}
