using ProjectF.Data.Entities.Clients;
using System;

namespace ProjectF.Api.Features.ContactClient
{
    public class ClientViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Rnc { get; set; }
        public DateTime? Birthday { get; set;}
        public string HomeOrApartment { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int SelectedCountry { get; set; }


        public ClientDto ToDto()
       => new ClientDto(Id,
                        Code,
                        Name,
                        Email,
                        Phone,
                        Rnc,
                        Birthday,
                        HomeOrApartment,
                        City,
                        Street,
                        SelectedCountry, null);
        public static ClientViewModel FromDto(ClientDto client)
            => new ClientViewModel()
            {
                Id = client.Id,
                Code = client.Code,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Rnc = client.Rnc,
                Birthday = client.Birthday,
                HomeOrApartment = client.HomeOrApartment,
                City = client.City,
                Street = client.Street,
                SelectedCountry = client.SelectedCountry
            };

    }
}
