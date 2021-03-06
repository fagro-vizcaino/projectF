﻿using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Auth
{
    public record RegisterUserDto(string? Id
          , string FirstName
          , string LastName
          , string UserName
          , string Password
          , string Email
          , string? PhoneNumber
          , ICollection<string> Roles
          , int SelectedCountry
          , Country? Country);

    public record UserLoginDto(string Email, string Password);
    public record AuthResponseDto(bool IsAuthSuccessful, string ErrorMessage, string Token);
    public record UserForgotPasswordDto(string Email);
    public record UserResetPasswordDto(string Password, string ConfirmPassword);

    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }

}
