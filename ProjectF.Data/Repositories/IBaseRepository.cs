using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LanguageExt;

namespace ProjectF.Data.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T entity);
        void Attach(T entity);
        void Delete(T element);
        IEnumerable<T> Filter(Expression<Func<T, bool>> predicate);
        Option<T> Find(params object[] keyValues);
        ValueTask<T> FindAsync(params object[] keyValues);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Option<T> Get(long id);
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        Task<Option<T>> GetAsync(int id);
        T GetSavedEntry(T entity);
        void Save();
        Task<int> SaveAsync();
    }
}