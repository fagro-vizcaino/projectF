using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Auth
{
    public class UserDto
  {
    public long Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public int SelectedCountry { get; set; }
    public Country Country { get; set; }

    public UserDto(long id
      , string email
      , string fullname
      , int selectedCountry
      , Country country
      , string password
      , byte[] passwordHash
      , byte[] passwordsalt)
    {
      Id              = id;
      Email           = email;
      Fullname        = fullname;
      SelectedCountry = selectedCountry;
      Country         = country;
      Password        = password;
      PasswordHash    = passwordHash;
      PasswordSalt    = passwordsalt;
    }

    public UserDto With(long? id = null
      , Email? email = null
      , string? fullname = null
      , int? selectedCountry = null
      , Country? country = null
      , string? password = null
      , byte[]? passwordHash = null
      , byte[]? passwordSalt = null)
          => new UserDto(id ?? this.Id
            , email ?? this.Email
            , fullname ?? this.Fullname
            , selectedCountry ?? this.SelectedCountry
            , country ?? this.Country
            , password ?? this.Password
            , passwordHash ?? this.PasswordHash
            , passwordSalt ?? this.PasswordSalt);
        
  }
}