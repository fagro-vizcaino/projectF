using ProjectF.Data.Context;
using ProjectF.Data.Entities.Invoices;

namespace ProjectF.Data.Repositories
{
    public class InvoiceRepository : _BaseRepository<InvoiceHeader>
    {
        readonly Context._AppDbContext _context;
        public InvoiceRepository(Context._AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
