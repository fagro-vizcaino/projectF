using ProjectF.Data.Context;
using ProjectF.Data.Entities.PaymentMethods;

namespace ProjectF.Data.Repositories
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethod>
    {
        readonly _AppDbContext _context;
        public PaymentMethodRepository(_AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
