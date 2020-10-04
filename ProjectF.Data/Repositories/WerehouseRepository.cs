using ProjectF.Data.Context;
using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Werehouses;

namespace ProjectF.Data.Repositories
{
    public class WerehouseRepository : _BaseRepository<Werehouse>
    {
        private readonly Context._AppDbContext _context;

        public WerehouseRepository(Context._AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}