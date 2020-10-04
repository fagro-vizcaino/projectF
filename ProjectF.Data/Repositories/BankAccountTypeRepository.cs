using ProjectF.Data.Entities.Banks;
using LanguageExt;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ProjectF.Data.Repositories
{
    public class BankAccountTypeRepository : _BaseRepository<BankAccountType>
    {
        readonly Context._AppDbContext _context;

        public BankAccountTypeRepository(Context._AppDbContext context) : base(context)
           => _context = context;

        public Option<bool> MustBeUnique(Expression<Func<BankAccountType, bool>> predicate)
            => _context.BankAccountTypes.Count(predicate) == 1;

    }
}
