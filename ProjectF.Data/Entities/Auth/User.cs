using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Auth
{
  public class User : Entity
  {
    public string Fullname { get; set; }
    public Email Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public virtual Country Country { get; set; }


    protected User() { }

    public User(string fullname
      , Email email
      , Country country
      , byte[] PasswordHash
      , byte[] PasswordSalt)
    {
      this.Fullname = fullname;
      this.Email = email;
      this.Country = country;
      this.PasswordHash = PasswordHash;
      this.PasswordSalt = PasswordSalt;
    }

    public void EditUser(string fullname
      , Email email
      , Country country
      , byte[] passwordHash
      , byte[] passwordSalt)
    {
      Fullname = fullname;
      Email = email;
      PasswordHash = passwordHash;
      PasswordSalt = passwordSalt;
    }

  }
}