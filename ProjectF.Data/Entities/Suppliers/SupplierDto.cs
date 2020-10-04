using ProjectF.Data.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.Suppliers
{
    public class SupplierDto
    {
        public long Id {get; set;}
        public string Code { get; }
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Rnc { get; }
        public string HomeOrApartment { get; }
        public string City { get; }
        public string Street { get; }
        public int SelectedCountry { get; }
        public Country Country { get; }
        public bool IsIndependent { get; }

        public SupplierDto(long id,
            string code
            , string name
            , string email
            , string phone
            , string rnc
            , string homeOrApartment
            , string city
            , string street
            , int selectedCountry
            , Country country
            , bool isIndependent)
        {
            Id              = id;
            Code            = code;
            Name            = name;
            Email           = email;
            Phone           = phone;
            Rnc             = rnc;
            HomeOrApartment = homeOrApartment;
            City            = city;
            Street          = street;
            SelectedCountry = selectedCountry;
            Country         = country;
            IsIndependent   = isIndependent;
        }


         public SupplierDto With(long? id = null
             , string? code = null
            , string? name = null
            , string? email = null
            , string? phone = null
            , string? rnc = null
            , string? homeOrApartment = null
            , string? city = null
            , string? street = null
            , int? selectedCountry = null
            , Country? country = null
            , bool? isIndependent = null)
        {
            return new SupplierDto(id ?? this.Id
                , code ?? this.Code
                , name ?? this.Name
                , email ?? this.Email
                , phone ?? this.Phone
                , rnc ?? this.Rnc
                , homeOrApartment ?? this.HomeOrApartment
                , city ?? this.City
                , street ?? this.Street
                , selectedCountry ?? this.SelectedCountry
                , country ?? this.Country
                , isIndependent ?? this.IsIndependent);
        }
    }
}
