using System.Collections.Generic;
using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.Auth
{
    public class RegisterUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public int SelectedCountry { get; set; }
        public Country Country { get; set; }
    }
}
