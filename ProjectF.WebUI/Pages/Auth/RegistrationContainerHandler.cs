using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;

namespace ProjectF.WebUI.Pages.Auth
{
    public class RegistrationContainerHandler : ComponentBase
    {
        protected UserRegisterDto _model = new();
        public Country[] Countries { get; set; } = Array.Empty<Country>();
        [Parameter] public string Token { get; set; }
        [Parameter] public string Email { get; set; }
        [Inject] public IBaseDataService<Country> CountryDataService { get; set; }
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Countries = (await CountryDataService.GetAll()).ToArray();
        }

        public async Task Register()
        {
            ShowRegistrationErros = false;
            var result = await AuthenticationService.RegisterUser(_model);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
