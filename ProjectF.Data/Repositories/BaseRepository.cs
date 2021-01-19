using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using ProjectF.Data.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly _AppDbContext _context;

        public BaseRepository(_AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual Option<T> Find(params object[] keyValues)
            => _context.Set<T>().Find(keyValues);

        public virtual async Task<Option<T>> GetAsync(int id) =>
           await _context.Set<T>().FindAsync(id);

        public virtual Option<T> Get(long id) => _context.Set<T>().Find(id);

        public virtual ValueTask<T> FindAsync(params object[] keyValues)
            => _context.Set<T>().FindAsync(keyValues);

        public virtual IEnumerable<T> GetAll()
           => _context.Set<T>().Select(c => c).AsEnumerable();

        public async Task<List<T>> GetAllAsync()
          => await _context.Set<T>().ToListAsync();

        public IEnumerable<T> Filter(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate).AsEnumerable();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
            => !trackChanges
            ? _context.Set<T>()
               .Where(expression)
               .AsNoTracking()
            : _context.Set<T>()
               .Where(expression);

        public void Delete(T element)
        {
            _context.Set<T>().Remove(element);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Attach(T entity)
        {
            _context.Attach(entity);
        }

        public T GetSavedEntry(T entity)
        {
            _context.Entry(entity).GetDatabaseValues();
            return entity;
        }



    }
}
