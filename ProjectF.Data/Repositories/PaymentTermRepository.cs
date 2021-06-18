using ProjectF.Data.Context;
using ProjectF.Data.Entities.PaymentList;

namespace ProjectF.Data.Repositories
{
    public class PaymentTermRepository : BaseRepository<PaymentTerm>
    {
        readonly _AppDbContext _context;
        public PaymentTermRepository(_AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
