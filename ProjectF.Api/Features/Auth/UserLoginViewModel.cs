using ProjectF.Data.Entities.Auth;
using ProjectF.Application.Auth;

namespace ProjectF.Api.Features.Auth
{
  public class UserLoginViewModel
  {
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserDto ToDto() => new UserDto(id:0
        , email: Email
        , fullname: string.Empty
        , selectedCountry: 0
        , country: null
        , password: Password
        , passwordHash: null
        , passwordsalt: null);
    public static UserLoginViewModel FromDto(UserDto user)
        => new UserLoginViewModel()
        {
          Email = user.Email,
          Password = user.Password,
          Fullname = user.Fullname
        };
  }
}