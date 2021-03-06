﻿using ProjectF.Data.Entities.Core;
using ProjectF.Data.Entities.Countries;
using ProjectF.Data.Entities.PaymentList;

namespace ProjectF.Data.Entities.Suppliers
{
    public record SupplierDto (string Code
        , string Name
        , string Email
        , string Phone
        , string Rnc
        , string HomeOrApartment
        , string City
        , string Street
        , int SelectedCountry
        , Country? Country
        , bool IsIndependent
        , SupplierGroup SupplierGroup
        , int PaymentTermId
        , PaymentTermDto? PaymentTerm
        , string Notes) : FDto;
}
