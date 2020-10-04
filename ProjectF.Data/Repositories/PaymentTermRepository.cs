using ProjectF.Data.Context;
using ProjectF.Data.Entities.PaymentList;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Repositories
{
    public class PaymentTermRepository : _BaseRepository<PaymentTerm>
    {
        readonly Context._AppDbContext _context;
        public PaymentTermRepository(Context._AppDbContext context): base(context)
        {
            _context = context;
        }
    }
}
