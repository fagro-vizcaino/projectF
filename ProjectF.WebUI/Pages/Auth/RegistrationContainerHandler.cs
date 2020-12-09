using System;
using System.Collections.Generic;
using System.Linq;
using static System.Text.Json.JsonSerializer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectF.WebUI.Models;
using ProjectF.WebUI.Services;
using System.Text;
using ProjectF.WebUI.Components.Common;

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
        [Inject] public IFMessage FMessage { get; set; }
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Countries = (await CountryDataService.GetAll()).ToArray();
        }

        public async Task Register(EditContext context)
        {
            UserRegisterDto model = context.Model as UserRegisterDto;
            model = AssignUsername(model);
            model = AssignUserRole(model);

            ShowRegistrationErros = false;
            var result = await AuthenticationService.RegisterUser(model);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErros = true;
            }
            else
            {
                await FMessage.Success($"Registro completado, favor verificar su email", 8);
            }
        }

        public UserRegisterDto AssignUsername(UserRegisterDto dto)
        {
            dto.UserName = dto.Email;
            return dto;
        }

        public UserRegisterDto AssignUserRole(UserRegisterDto dto)
        {
            dto.Roles = new [] {"Admin" };
            return dto;
        }

        public void OnFinishFailed(EditContext editContext)
        {
            Console.WriteLine($"Failed:{Serialize(editContext.Model)}");
        }

        public async Task ConfirmedEmail()
        {
            ShowRegistrationErros = false;
            var result = await AuthenticationService.ConfirmedEmail(Token, Email);
            Console.WriteLine($"returned values from email confirmation {result}");
            if (result >= 1)
            {
                NavigationManager.NavigateTo($"/signin");
            }
            else
            {
                //Show Errors
            }
        }
    }
}
