using ProjectF.Data.Context;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Repositories
{
    public class CountryRepository : _BaseRepository<Country>
    {
        private readonly _AppDbContext _context;

        public CountryRepository(_AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public Country FromCountryId(int id)
          => _context.Countries.Find(id);
    }
}