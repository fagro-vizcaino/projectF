using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;
using System;

namespace ProjectF.Data.Entities.Clients
{
    public record ClientDto(long Id, 
        string Code, 
        string Firstname, 
        string Lastname, 
        string Email, 
        string Phone,
        string Rnc, 
        DateTime? Birthday, 
        string HomeOrApartment, 
        string City, 
        string Street, 
        int SelectedCountry, 
        Country? Country);
}