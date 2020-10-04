using ProjectF.Application.Auth;
using ProjectF.Data.Entities.Countries;
using ProjectF.Data.Entities.Auth;

namespace ProjectF.Api.Features.Auth
{
  public class UserRegisterViewModel
  {
    public long Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public int SelectedCountry { get; set; }
    public string Password { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public UserDto ToDto() 
        => new UserDto(Id
                  , Email
                  , Fullname
                  , SelectedCountry
                  , null
                  , Password
                  , PasswordHash, PasswordSalt);
    public static UserRegisterViewModel FromDto(UserDto user)
        => new UserRegisterViewModel()
        {
          Id = user.Id,
          Email = user.Email,
          Fullname = user.Fullname,
          SelectedCountry = user.SelectedCountry,
          Password = user.Password,
          PasswordHash = user.PasswordHash,
          PasswordSalt = user.PasswordSalt
        };
  }
}