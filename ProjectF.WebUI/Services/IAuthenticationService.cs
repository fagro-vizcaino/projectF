using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Pages.Auth;

namespace ProjectF.WebUI.Services
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserRegisterDto userForRegistration);
        Task<int> ConfirmedEmail(string token, string email);
        Task<bool> ForgotPassword(UserForgotPasswordDto forgotPasswordDto);
        Task<bool> ResetPassword(string token, string email, UserResetPasswordDto userResetPasswordDto);
        Task<AuthReponseDto> SignIn(UserLoginDto userLoginDto);
    }
}
