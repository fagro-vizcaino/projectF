using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Context;
using ProjectF.Data.Entities.UnitOfMeasures;

namespace ProjectF.Data.Repositories
{
    public class UnitOfMeasureRepository : BaseRepository<UnitOfMeasure>
    {
        readonly _AppDbContext _context;

        public UnitOfMeasureRepository(_AppDbContext context) : base(context)
            => _context = context;
    }
}
