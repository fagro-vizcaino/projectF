using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using Microsoft.EntityFrameworkCore;
using ProjectF.Data.Context;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Entities.RequestFeatures;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProjectF.Data.Repositories
{
    public class InvoiceRepository : BaseRepository<InvoiceHeader>
    {
        readonly _AppDbContext _context;
        public InvoiceRepository(_AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Either<Error, InvoiceHeader>> FindByAsync(params object[] valuesKeys)
        {
            var result = await base.FindAsync(valuesKeys);
            if (result is null) return Error.New("No Invoice Found");

            _context.Entry(result).Reference(c => c.Client).Load();
            _context.Entry(result).Reference(c => c.PaymentTerm).Load();
            _context.Entry(result).Collection(c => c.InvoiceDetails).Load();

            return result;
        }

        public Option<InvoiceHeader> FindBy(params object[] valuesKeys)
            => base.Find(valuesKeys).Match(c =>
            {
                _context.Entry(c).Reference(c => c.Client).Load();
                _context.Entry(c).Reference(c => c.PaymentTerm).Load();
                _context.Entry(c).Collection(c => c.InvoiceDetails).Load();

                return c;
            }, () => Option<InvoiceHeader>.None);

        public async Task<Either<Error, PagedList<InvoiceHeader>>> GetInvoiceMainListAsync(InvoiceListParameters paramenters,
            bool trackChanges)
        {
            try
            {
                var invoices = await FindByCondition(e => e.InvoiceDate >= paramenters.DateFrom
                && e.DueDate <= paramenters.DateTo, trackChanges)
                .Include(c => c.Client)
                .OrderBy(e => e.Id)
                .ToListAsync();

                return PagedList<InvoiceHeader>
                  .ToPagedList(invoices, paramenters.PageNumber, paramenters.PageSize);
            }
            catch (System.Exception ex)
            {
                return Error.New(ex.Message);
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            using var transaction = _context.Database.BeginTransaction();
            return transaction;
        }

        public Either<Error, Unit> EndTransaction()
        {
            try
            {
                _context.Database.CommitTransaction();
                return Right(Unit.Default);
            }
            catch (System.Exception e)
            {
                return Left<Error>(Error.New(e.Message));   
            }
        }
    }
}
