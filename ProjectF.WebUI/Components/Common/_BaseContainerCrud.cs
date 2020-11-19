using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Components.Common
{
    public class BaseContainerBasicCrud<T> : ComponentBase, IBaseContainerCrud<T> where T : FEntity
    {
        private readonly string _entityName;

        [Inject]
        public IBaseDataService<T> DataService { get; set; }   
        [Inject]
        public IFMessage FMessage { get; set; }
        public T[] Elements { get; set; }
        public Func<T, T> NewOrEditOperation { get; set;}

        public bool IsDrawerVisible { get; set; } = false;
        public bool IsEditing { get; private set; } = false;
        public string GenerateCode => Guid.NewGuid().ToString().Substring(0, 6);
        
        public T _model = null;
        
        public BaseContainerBasicCrud(string entityName, 
            T[] entities = null)
            => (_entityName, Elements) = (entityName, entities ?? Array.Empty<T>());

        protected T InitModel(T model) => _model = model;

        protected (bool isDrawervisible, bool isEdition) GetDrawerState(T entity)
            => (true, entity.Id > 0);

        protected void OpenDrawerForAdd(T entity)
        {
            _model = entity;
            var (isDrawerVisible, isEdition) = GetDrawerState(entity);
            IsEditing = isEdition;
            IsDrawerVisible = isDrawerVisible;
        }

        protected void OpenDrawerForEdit(T entity)
        {
            var newCategory = NewOrEditOperation(entity);
            var (isDrawerVisible, isEdition) = GetDrawerState(entity);
            IsDrawerVisible = isDrawerVisible;
            IsEditing = isEdition;
            _model = newCategory;
        }

        protected void CloseDrawer()
            => (IsDrawerVisible, IsEditing) = (false, false);

        protected override async Task OnInitializedAsync()
            => await GetAll();

        public virtual async Task OnFinish(EditContext editContext, long id)
        {
            if (id > 0)
            {
                await Edit(id, (T)editContext.Model);
            }
            else
            {
                await Add((T)editContext.Model);
            }
        }

        public virtual void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{JsonSerializer.Serialize(editContext.Model)}");
        }

        public virtual async Task GetAll()
        {
            var result = (await DataService.GetAll()).ToArray();
            //Console.WriteLine($"Get all:{JsonSerializer.Serialize(result)}");
            Elements = result;
        }

        public virtual async Task<T> GetById(int id)
        {
            var entity = (await DataService.GetDetails(id));
            //Console.WriteLine($"Get detail :{JsonSerializer.Serialize(entity)}");
            return entity;
        }
        public virtual async Task<Either<Error, Unit>> Edit(long id, T entity)
        {
            //Console.WriteLine($"Success editing:{JsonSerializer.Serialize(entity)}");
            return
                await DataService.Update(id, entity)
                .Match(
                    Some: async c =>
                    {
                        Elements = Elements
                        .Where(x => x.Id != id)
                        .Concat(new List<T> { c })
                        .ToArray();
                        CloseDrawer();
                        await FMessage.Success($"{_entityName} editado", 3);

                    },
                   None: () => Error.New("Error while updating"));
        }

        public virtual async Task<Either<Error, Unit>> Add(T entity)
        {
            Console.WriteLine($"About to add:{JsonSerializer.Serialize(entity)}");
            return await DataService.Add(entity)
                .Match(async c =>
                {
                    Elements = Elements.Concat(new List<T> { entity }).ToArray();
                    CloseDrawer();
                    await FMessage.Success($"{_entityName} guardado", 3);
                }, () => Error.New("Error while creating"));
        }

        public virtual async Task Delete(long id)
        {
            await DataService.Delete(id)
                .Match(Some: async c =>
                {
                    Elements = Elements.Where(e => e.Id != id).ToArray();
                     CloseDrawer();
                    await FMessage.Success($"{_entityName} borrada", 3);
                },
                None: () => { });
        }

    }
}
