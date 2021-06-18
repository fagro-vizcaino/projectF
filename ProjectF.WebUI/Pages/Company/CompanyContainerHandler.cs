using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;

namespace ProjectF.WebUI.Pages.Company
{
    public class CompanyContainerHandler : BaseContainerBasicCrud<Company>
    {
        public Country[] Countries { get; set; } = Array.Empty<Country>();

        public CompanyContainerHandler() : base("Empresa")
        {
            Company emptyObject = new();
            InitModel(emptyObject);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        [Inject]
        public IBaseDataService<Country> CountryDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();
            Countries = (await CountryDataService.GetAll()).ToArray();
        }

        public Company GetNewModelOrEdit(Company company = null)
           => company ?? new Company
           {
               Name = null
               , City = null
               , HomeOrApartment = null
               , Id = 0
               , Phone = null
               , Rnc = null
               , SelectedCountry = 0
               , Street = null
               , Website = null
           };
    }
}
