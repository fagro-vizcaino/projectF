using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Services;

namespace ProjectF.WebUI.Pages.Auth
{
    public class SignInContainerHandler : ComponentBase
    {
        protected UserLoginDto _model = new();

        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IFMessage FMessage { get; set; }
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool IsSubmitting { get; set; } = false;

        public async Task SignIn(EditContext context)
        {
            IsSubmitting = true;
            var model = context.Model as UserLoginDto;
           

            ShowRegistrationErros = false;
            var result = await AuthenticationService.SignIn(model);
            //if (!result.IsSuccessfulRegistration)
            //{
            //    Errors = result.Errors;
            //    ShowRegistrationErros = true;
            //}
            //else
            //{
            //    IsSubmitting = false;
            //    NavigationManager.NavigateTo("/");

            //}
        }


        public void OnFinishFailed()
        {

        }
    }
}
