using System;

namespace ProjectF.WebUI.Models
{
    public class Client : FEntity
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Rnc { get; set; }
        public DateTime? Birthday { get; set; }
        public string HomeOrApartment { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int SelectedCountry { get; set; }
        public string DisplayName => $"{FirstName} {Lastname}";
    }
}
