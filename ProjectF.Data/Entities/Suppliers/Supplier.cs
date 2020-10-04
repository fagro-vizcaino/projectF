using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Suppliers
{
    public class Supplier : Entity
    {

        public Code Code { get; private set; }
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public string Rnc { get; private set; }
        public string HomeOrApartment { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public virtual Country Country { get; private set; }
        public bool IsIndependent { get; private set; }

        protected Supplier() { }

        public Supplier(Code code
            , Name name
            , Email email
            , Phone phone
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , Country country
            , bool IsIndependent)
        {
            Code = code;
            Name = name;
            Email = email;
            Phone = phone;
            Rnc = rnc;
            HomeOrApartment = homeOrApartment;
            City = city;
            Street = street;
            Country = country;
            this.IsIndependent = IsIndependent;
        }

        public void EditSupplier(
             Code code
            , Name name
            , Email email
            , Phone phone
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , Country country
            , bool isIndependent)
        {
            Code = code;
            Name = name;
            Email = email;
            Phone = phone;
            Rnc = rnc;
            HomeOrApartment = homeOrApartment;
            City = city;
            Street = street;
            IsIndependent = isIndependent;
            Country = country;
        }
    }
}
