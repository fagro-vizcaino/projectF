using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Context;
using ProjectF.Data.Entities.PurchaseOrders;

namespace ProjectF.Data.Repositories
{
    public class PurchaseOrderRepository : BaseRepository<PurchaseOrderHeader>
    {
        readonly _AppDbContext _context;

        public PurchaseOrderRepository(_AppDbContext context) : base(context)
           => _context = context;

        public async Task<Either<Error, PurchaseOrderHeader>> FindByAsync(params object[] valuesKeys)
        {
            var result = await base.FindAsync(valuesKeys);
            if (result is null) return Error.New("No purchase order found");
            return result;
        }

    }
}
