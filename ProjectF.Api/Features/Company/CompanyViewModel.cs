using ProjectF.Api.Features.Countries;
using ProjectF.Api.Features.TaxRegime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Api.Features.Companies
{
    public class CompanyViewModel
    {
        public string Name { get; private set; }
        public string Rnc { get; private set; }
        public string HomeOrApartment { get; private set; }
        public string City {get; private set;}
        public string Street { get; private set; }
        public CountryViewModel Country { get; private set;}
        public string Phone { get; private set; }
        public string Website { get; private set; }
        public TaxRegimeTypeViewModel RegimeType { get; private set; }
        //public CurrencyViewModel Currency {get; private set;}
    }
}
