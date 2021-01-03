using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Countries;

namespace ProjectF.Data.Entities.Companies
{
    public record CompanyDto(long Id
        , string Name
        , string Rnc
        , string HomeOrApartment
        , string City
        , string Street
        , Country Country
        , string Phone
        , string Website
        , DateTime Created
        , EntityStatus Status);
}
