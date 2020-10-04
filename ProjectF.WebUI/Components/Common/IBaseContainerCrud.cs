using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Components.Common
{
    public interface IBaseContainerCrud<T> where T : class
    {
        public T[] Elements { get; set; }
        Task OnFinish(EditContext editContext, long id);
        void OnFinishFailed(EditContext editContext);
        Task GetAll();
        Task<Either<Error, Unit>> Edit(long id, T element);
        Task<Either<Error, Unit>> Add(T element);
        Task Delete(long id);
    }
}
