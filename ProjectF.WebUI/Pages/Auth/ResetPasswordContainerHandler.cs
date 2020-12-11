using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Services;

namespace ProjectF.WebUI.Pages.Auth
{
    public class ResetPasswordContainerHandler : ComponentBase
    {
        public UserResetPasswordDto Model { get; set; } = new UserResetPasswordDto();

        [Parameter] public string Token { get; set; }
        [Parameter] public string Email { get; set; }
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public IFMessage FMessage { get; set; }
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool IsSubmitting { get; set; } = false;

        public async Task ResetPassword(EditContext context)
        {
            IsSubmitting = true;
            var model = context.Model as UserResetPasswordDto;

            ShowRegistrationErros = false;
            var result = await AuthenticationService.ResetPassword(Token, Email, model);
            if (result)
            {
                await FMessage
                    .Success($"Contraseña ha sido actualizada, puedes iniciar sesion", 8);
                Model = new UserResetPasswordDto(string.Empty, string.Empty);
                IsSubmitting = false;
            }
        }

        public void OnFinishFailed()
        {

        }
    }
}
