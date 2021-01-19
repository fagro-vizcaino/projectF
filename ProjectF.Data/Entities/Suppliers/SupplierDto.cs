using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Countries;
using System;

namespace ProjectF.Data.Entities.Suppliers
{
    public record SupplierDto
        (long Id
        , string Code
        , string Name
        , string Email
        , string Phone
        , string Rnc
        , string HomeOrApartment
        , string City
        , string Street
        , int SelectedCountry
        , Country Country
        , SupplierGroup SupplierGroup
        , bool IsIndependent
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
