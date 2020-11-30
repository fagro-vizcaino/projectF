using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Auth
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual Country Country { get; set; }
        public long? CompanyId { get; protected set; }
        
        protected User() { }

        public User(string firstname
            , string lastname
            , string username
            , string passwordhash
            , string email
            , string phoneNumber
            , Country country)
        {
            Firstname         = firstname;
            Lastname          = lastname;
            UserName          = username;
            PasswordHash      = passwordhash;
            Email             = email;
            PhoneNumber       = phoneNumber;
            Country           = country;
            
        }

        public void EditUser(string firstname
          , string lastname)
        {
            Firstname = firstname;
            Lastname  = lastname;
        }

    }
}