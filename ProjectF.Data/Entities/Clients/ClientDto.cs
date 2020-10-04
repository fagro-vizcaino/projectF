using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Countries;
using System;

namespace ProjectF.Data.Entities.Clients
{
    public class ClientDto
    {
        public long Id { get; }
        public string Code { get; }
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Rnc { get; }
        public DateTime? Birthday { get; }
        public string HomeOrApartment { get; }
        public string City { get; }
        public string Street { get; }
        public int SelectedCountry { get; }
        public Country Country { get; }

        public ClientDto(long id, 
            string code,
            string name,
            string email,
            string phone,
            string rnc,
            DateTime? birthday,
            string homeorApartment,
            string city,
            string street,
            int selectedCountry,
            Country country)
        {
            Id              = id;
            Code            = code;
            Name            = name;
            Email           = email;
            Phone           = phone;
            Rnc             = rnc;
            Birthday        = birthday;
            HomeOrApartment = homeorApartment;
            City            = city;
            Street          = street;
            SelectedCountry = selectedCountry;
            Country         = country;
        }


        public ClientDto With(long? id = null,
            string? code = null,
            string? name = null,
            string? email = null,
            string? phone = null,
            string? rnc = null,
            DateTime? birthday = null,
            string? homeOrApartment = null,
            string? city = null,
            string? street = null,
            int? selectedCountry = null,
            Country? country = null)
        {
            return new ClientDto(id ?? this.Id,
                code ?? Code,
                name ?? Name,
                email ?? Email,
                phone ?? Phone,
                rnc ?? Rnc,
                birthday ?? Birthday,
                homeOrApartment ?? HomeOrApartment,
                city ?? City,
                street ?? Street,
                selectedCountry ?? SelectedCountry,
                country ?? Country);
        }

        public static implicit operator Client(ClientDto dto)
            => new Client(new Code(dto.Code),
                new Name(dto.Name),
                new Email(dto.Email),
                new Phone(dto.Phone),
                dto.Birthday,
                dto.Rnc,
                dto.HomeOrApartment,
                dto.City,
                dto.City,
                dto.Country);

    }
}