using ProjectF.Data.Context;
using ProjectF.Data.Entities.Categories;

namespace ProjectF.Data.Repositories
{
  public class CategoryRepository : BaseRepository<Category>
  {
    private readonly _AppDbContext _context;

    public CategoryRepository(_AppDbContext context) : base(context)
    {
      _context = context;
    }
  }
}