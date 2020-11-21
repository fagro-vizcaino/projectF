using ProjectF.Data.Entities.Banks;
using LanguageExt;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt.Common;
using ProjectF.Data.Entities.RequestFeatures;
using System;

namespace ProjectF.Data.Repositories
{
    public class BankAccountRepository : _BaseRepository<BankAccount>
    {
        readonly Context._AppDbContext _context;

        public BankAccountRepository(Context._AppDbContext context) : base(context)
            => _context = context;

        public Option<bool> MustBeUnique(string predicate)
            => _context.BankAccounts.Where(c => c.AccountNumber == predicate).Count() == 1;
        //like operation_context.BankAccounts.Where(b => EF.Functions.Like(b.AccountName.Value, $"%{name}%"));

        public override Option<BankAccount> Get(long id)
            => _context.BankAccounts
                .Include(b => b.BankAccountType)
                .SingleOrDefault(b => b.Id == id);

        public override IEnumerable<BankAccount> GetAll()
        => _context.BankAccounts
                .Include(b => b.BankAccountType)
                .ToList();


        public async Task<Either<Error, PagedList<BankAccount>>> GetBankAccountListAsync(BankListParameters paramenters, bool trackChanges)
        {
            try
            {
                var bankAccount = await FindByCondition(e => e.Id > 0, trackChanges)
                    .Include(b => b.BankAccountType)
                    .OrderBy(e => e.Id)
                    .ToListAsync();

                return PagedList<BankAccount>
                  .ToPagedList(bankAccount, paramenters.PageNumber, paramenters.PageSize);
            }
            catch (Exception ex)
            {
                return Error.New(ex.Message);
            }
        }

    }
}
