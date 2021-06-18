using ProjectF.Data.Context;
using ProjectF.Data.Entities.GoodsTypes;

namespace ProjectF.Data.Repositories
{
    public class GoodsTypeRepository : BaseRepository<GoodsType>
    {
        readonly _AppDbContext _context;

        public GoodsTypeRepository(_AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
