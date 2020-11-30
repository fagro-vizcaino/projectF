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
    public class ProductRepository : _BaseRepository<Product>
    {
        readonly _AppDbContext _context;
        public ProductRepository(_AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override IEnumerable<Product> GetAll()
        {
            return _context.Products
                 .Include(u => u.Category)
                 .Include(u => u.Warehouse)
                 .Include(u => u.Tax)
                 .Select(c => c).AsEnumerable();
        }

        public async Task<Either<Error, PagedList<Product>>> GetProductListAsync(ProductListParameters paramenters, bool trackChanges)
        {
            try
            {
                var products = await FindByCondition(e => e.Id > 0, trackChanges)
                    .Include(u => u.Category)
                    .Include(u => u.Warehouse)
                    .Include(u => u.Tax)
                    .OrderBy(e => e.Id)
                    .ToListAsync();

                return PagedList<Product>
                  .ToPagedList(products, paramenters.PageNumber, paramenters.PageSize);
            }
            catch (Exception ex)
            {
                return Error.New(ex.Message);
            }
        }

        public override Option<Product> Find(params object[] keyValues)
        {
            var product = _context.Products.Find(keyValues);

            _context.Entry(product).Reference(c => c.Category).Load();
            _context.Entry(product).Reference(c => c.Warehouse).Load();
            _context.Entry(product).Reference(c => c.Tax).Load();
            return product;
        }

        public Product GetByKeys(long id)
         => _context.Products
                .Include(c => c.Category)
                .Include(c => c.Warehouse)
                .Include(c => c.Tax)
                .FirstOrDefault(c => c.Id == id);
    }
}
