using ProjectF.Data.Context;
using ProjectF.Data.Products;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;
using ProjectF.Data.Entities.Clients;
using LanguageExt.Common;
using ProjectF.Data.Entities.Products;
using System;

namespace ProjectF.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        private readonly _AppDbContext _context;

        public ProductRepository(_AppDbContext context) : base(context)
            => _context = context;

        public override IEnumerable<Product> GetAll()
        {
            return _context.Products
                 .Include(u => u.Category)
                 .Include(u => u.Warehouse)
                 .Include(u => u.Tax)
                 .Select(c => c).AsEnumerable();
        }

        public async Task<Option<PagedList<Product>>> GetProductListAsync(ProductListParameters paramenters, bool trackChanges)
        {
            
                var products = await FindByCondition(e => e.Id > 0, trackChanges)
                    .Include(u => u.Category)
                    .Include(u => u.Warehouse)
                    .Include(u => u.Tax)
                    .Include(u => u.UnitOfMeasure)
                    .OrderBy(e => e.Id)
                    .ToListAsync();

                return PagedList<Product>
                  .ToPagedList(products, paramenters.PageNumber, paramenters.PageSize);
        }

        public override Option<Product> Find(params object[] keyValues)
        {
            var product = _context.Products.Find(keyValues);

            _context.Entry(product).Reference(c => c.Category).Load();
            _context.Entry(product).Reference(c => c.Warehouse).Load();
            _context.Entry(product).Reference(c => c.Tax).Load();
            return product;
        }

        public async Task<Option<Product>> GetByKeys(long id)
         => await _context.Products
                .Include(c => c.Category)
                .Include(c => c.Warehouse)
                .Include(c => c.Tax)
                .FirstOrDefaultAsync(c => c.Id == id);
    }
}
