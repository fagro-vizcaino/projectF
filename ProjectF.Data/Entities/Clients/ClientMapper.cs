using ProjectF.Data.Entities.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectF.Data.Entities.Clients
{
    public static class ClientMapper
    {
        public static Client FromDto(ClientDto dto)
            => new Client(new Code(dto.Code)
                , new Name(dto.Firstname)
                , new Name(dto.Lastname)
                , new Email(dto.Email)
                , new Phone(dto.Phone)
                , dto.Birthday
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , dto.Created
                , dto.Status);

        public static ClientDto FromEntity(Client model)
            => new ClientDto(model.Id,
                model.Code.Value,
                model.Firstname.Value,
                model.Lastname.Value,
                model.Email,
                model.Phone.Value,
                model.Rnc,
                model.Birthday,
                model.HomeOrApartment,
                model.City,
                model.Street,
                model.Country?.Id ?? 0,
                model.Country
                , model.Created
                , model.Modified
                , model.Status);
    }
}
