using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Auth;

namespace ProjectF.WebUI.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterUserDto> RegisterUser(RegisterUserDto userForRegistration);
    }
}
