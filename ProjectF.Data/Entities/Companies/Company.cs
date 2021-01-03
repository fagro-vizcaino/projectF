using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities
{
    public class Company : _BaseEntity
    {
        public Name Name { get; private set; }
        public string Rnc { get; private set; }
        public string HomeOrApartment { get; private set; }
        public string City {get; private set;}
        public string Street { get; private set; }
        public virtual Country Country { get; private set; }
        public Phone Phone { get; private set; }
        public string Website { get; private set; }

        protected Company() { }

        public Company(Name name
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , Country country
            , Phone phone
            , string website
            , long companyId
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Name            = name;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            Country         = country;
            Phone           = phone;
            Website         = website;
            CompanyId       = companyId;
            Created         = created == DateTime.MinValue ? DateTime.Now : created;
            Status          = status;
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
            , EntityStatus status)
        {
            Name            = name;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            Country         = country;
            Phone           = phone;
            Website         = website;
            Status          = status;
            Modified        = DateTime.Now;
        }
    }
}
