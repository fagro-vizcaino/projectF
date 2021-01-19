using ProjectF.Data.Context;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Warehouses;

namespace ProjectF.Data.Repositories
{
    public class WerehouseRepository : BaseRepository<Warehouse>
    {
        private readonly Context._AppDbContext _context;

        public WerehouseRepository(Context._AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}