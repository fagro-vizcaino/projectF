using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Clients
{
    public class ClientContainerHandler : BaseContainerBasicCrud<Client>
    {
        public Country[] Countries { get; set; } = Array.Empty<Country>();
        public ClientContainerHandler() : base("Cliente")
        {
            var emptyObject = new Client();
            InitModel(emptyObject);
            NewOrEditOperation = GetNewModelOrEdit;
        }

        [Inject]
        public IBaseDataService<Country> CountryDataService { get; set; }
        public BankAccountType[] BankAccountTypes { get; set; } = Array.Empty<BankAccountType>();
        protected override async Task OnInitializedAsync()
        {
            Elements = (await DataService.GetAll()).ToArray();

            Countries = (await CountryDataService.GetAll()).ToArray();
        }
        public Client GetNewModelOrEdit(Client client = null)
            => client ?? new Client
            {
                Code            = GenerateCode,
                City            = null,
                Email           = null,
                HomeOrApartment = null,
                Name            = null,
                Phone           = null,
                Rnc             = null,
                Birthday        = null,
                SelectedCountry = 0,
                Street          = null                
            };
    }
}
