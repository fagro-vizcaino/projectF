using ProjectF.Data.Entities.Banks;
using LanguageExt;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

    }
}
