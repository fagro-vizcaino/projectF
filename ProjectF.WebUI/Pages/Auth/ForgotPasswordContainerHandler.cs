using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Services;

namespace ProjectF.WebUI.Pages.Auth
{
    public class ForgotPasswordContainerHandler : ComponentBase
    {
        public UserForgotPasswordDto Model { get; set; } = new UserForgotPasswordDto();
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IFMessage FMessage { get; set; }
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool IsSubmitting { get; set; } = false;

        public async Task ForgotPassword(EditContext context)
        { 
            IsSubmitting = true;
            var model = context.Model as UserForgotPasswordDto;
            
            ShowRegistrationErros = false;
            var result = await AuthenticationService.ForgotPassword(model);
            if (result)
            {
                await FMessage
                    .Success($"Los pasos para cambiar su contraseña fueron enviados a su Email", 8);
                IsSubmitting = false;
            }
        }

        public void OnFinishFailed()
        {

        }
    }
}
