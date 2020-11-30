using System.Collections.Generic;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Auth
{
    public record RegisterUserDto(string Id
          , string FirstName
          , string LastName
          , string UserName
          , string Password
          , string Email
          , string PhoneNumber
          , ICollection<string> Roles
          , int SelectedCountry
          , Country Country);

    public record UserLoginDto(string Username, string Password);
    public record UserForgotPasswordDto(string Email);
    public record UserResetPasswordDto(string Password, string ConfirmPassword);

}
