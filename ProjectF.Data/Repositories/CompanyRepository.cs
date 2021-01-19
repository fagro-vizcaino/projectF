using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectF.Data.Context;
using ProjectF.Data.Entities;

namespace ProjectF.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>
    {
        readonly _AppDbContext _context;

        public CompanyRepository(_AppDbContext context) : base(context)
            => _context = context;

        public IEnumerable<Company> GetAll(string userId)
        {
            //var result =TableA.Join(TableB, left => left.Id, right => right.ForeignKeyToTableA, (left, right) => new { TableAColumns = left, TableBColumns = right });
            var companies = _context.Companies.Include(c => c.Country)
                .Join(_context.Users, left => left.CompanyId, 
                    right => right.CompanyId, (left, right) => new { _Comp = left, _User = right })
                .Where(c => c._User.Id == userId)
                .Select(c => c._Comp)
                .ToList();

            return companies;

        }


        //public virtual IEnumerable<T> GetAll()
        //   => _context.Set<T>().Select(c => c).AsEnumerable();

    }
}
