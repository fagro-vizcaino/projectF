using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ProjectF.WebUI.Components.Common;
using ProjectF.WebUI.Services;

namespace ProjectF.WebUI.Pages.Auth
{
    public class SignContainerHandler : ComponentBase
    {
        protected UserRegisterDto _model = new();
      
        [Inject] public IAuthenticationService AuthenticationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IFMessage FMessage { get; set; }
        public bool ShowRegistrationErros { get; set; }
        public IEnumerable<string> Errors { get; set; }

        protected override async Task OnInitializedAsync()
        {
        }

    }
}
