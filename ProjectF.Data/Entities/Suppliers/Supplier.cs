using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Suppliers
{
    public class Supplier : _BaseEntity
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
            , bool isIndependent
            , DateTime created
            , DateTime? modified = null
            , EntityStatus status = EntityStatus.Active)
        {
            Code            = code;
            Name            = name;
            Email           = email;
            Phone           = phone;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            Country         = country;
            IsIndependent   = isIndependent;
            Created         = created == DateTime.MinValue ? DateTime.Now : created;
            Modified        = modified;
            Status          = status;
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
            , bool isIndependent
            , EntityStatus status)
        {
            Code            = code;
            Name            = name;
            Email           = email;
            Phone           = phone;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            IsIndependent   = isIndependent;
            Country         = country;
            Status          = status;
            Modified        = DateTime.Now;
        }
    }
}
