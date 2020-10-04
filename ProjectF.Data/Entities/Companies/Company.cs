using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;
using ProjectF.Data.Entities.Currencies;
using ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType;

namespace ProjectF.Data.Entities
{
    public class Company : Entity
    {
        public Name Name { get; private set; }
        public string Rnc { get; private set; }
        public string HomeOrApartment { get; private set; }
        public string City {get; private set;}
        public string Street { get; private set; }
        public virtual Country Country { get; private set;}
        public Phone Phone { get; private set; }
        public string Website { get; private set; }
        public virtual TaxRegimeType RegimeType { get; private set; }
        public virtual Currency Currency {get; private set;}

        protected Company() { }

        public Company(Name name
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , Country country
            , Phone phone
            , string website
            , TaxRegimeType regimeType
            , Currency currency)
        {
            Name            = name;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            Country         = country;
            Phone           = phone;
            Website         = website;
            RegimeType      = regimeType;
            Currency        = currency;
        }

        public void EditCompany(
              Name name
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , Country country
            , Phone phone
            , string website
            , TaxRegimeType regimeType
            , Currency currency)
        {
            Name            = name;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            Country         = country;
            Phone           = phone;
            Website         = website;
            RegimeType      = regimeType;
            Currency        = currency;
        }


    }
}
