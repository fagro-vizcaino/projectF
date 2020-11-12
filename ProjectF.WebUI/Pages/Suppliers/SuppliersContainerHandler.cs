using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Suppliers
{
    public class SupplierContainerHandler : BaseContainerBasicCrud<Supplier>
    {
        public SupplierContainerHandler() : base("Suplidor")
        {
            var emtpyModel = new Supplier
            {
                Id = 0,
                Code = string.Empty,
                Name =string.Empty,
            };
            InitModel(emtpyModel);
            NewOrEditOperation = GetNewModelOrEdit;
            
        }

        [Inject]
        public IBaseDataService<Country> CountryDataService { get; set; }
        public Country[] Countries { get; set; } = Array.Empty<Country>();
        
        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();

            Countries = (await CountryDataService.GetAll()).ToArray();
        }

        public Supplier GetNewModelOrEdit(Supplier supplier = null)
            => supplier != null 
            ? new Supplier
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Code = supplier.Code,
                City = supplier.City,
                SelectedCountry = supplier.SelectedCountry,
                Email = supplier.Email,
                Country = supplier.Country,
                IsIndependent = supplier.IsIndependent,
                Rnc = supplier.Rnc,
                HomeOrApartment = supplier.HomeOrApartment,
                Phone = supplier.Phone,
                Street = supplier.Street
            }
            : new Supplier { Id = 0, Name = null, Code = GenerateCode};
    }
}
