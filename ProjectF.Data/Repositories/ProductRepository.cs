using ProjectF.Data.Context;
using ProjectF.Data.Products;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ProjectF.Data.Repositories
{
    public class ProductRepository : _BaseRepository<Product>
    {
        readonly Context._AppDbContext _context;
        public ProductRepository(Context._AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Product> GetAll()
        {
            return _context.Products
                 .Include(u => u.Category)
                 .Include(u => u.Werehouse)
                 .Include(u => u.Tax)
                 .Select(c => c).AsEnumerable();
        }

        public override Option<Product> Find(params object[] keyValues)
        {
            var product = _context.Products.Find(keyValues);

            _context.Entry(product).Reference(c => c.Category).Load();
            _context.Entry(product).Reference(c => c.Werehouse).Load();
            _context.Entry(product).Reference(c => c.Tax).Load();
            return product;
        }

        public Product GetByKeys(long id)
        {
            return _context.Products
                .Include(c => c.Category)
                .Include(c => c.Werehouse)
                .Include(c => c.Tax)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
