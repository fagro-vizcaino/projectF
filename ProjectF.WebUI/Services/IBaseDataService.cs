using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Services
{
    public interface IBaseDataService<T> where T : class
    {
        Task<Option<T>> Add(T item);
        Task<Option<T>> Update(long id, T item);
        Task<Option<string>> Delete(long id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetDetails(long id);
    }
}
