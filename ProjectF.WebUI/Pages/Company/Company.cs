using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.Company
{
    public class Company : FEntity
    {
        public long CompanyId { get; set; }
        public string Name { get; set; }
        public string Rnc { get; set; }
        public string HomeOrApartment { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int SelectedCountry { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public int Status { get; set; }

    }
}
