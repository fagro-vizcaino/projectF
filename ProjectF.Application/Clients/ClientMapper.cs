using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Clients
{
    public static class ClientMapper
    {
        public static Client FromDto(ClientDto dto)
            => new(new Code(dto.Code)
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
            => new(model.Id,
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
