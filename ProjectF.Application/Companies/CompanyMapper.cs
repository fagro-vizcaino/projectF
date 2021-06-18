using ProjectF.Data.Entities;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Companies;

namespace ProjectF.Application.Companies
{
    public static class CompanyMapper
    {
        public static Company FromDto(CompanyDto dto)
            => new Company(new Name(dto.Name)
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , new Phone(dto.Phone)
                , dto.Website
                , dto.Id
                , dto.Created
                , dto.Status);

        public static CompanyDto FromEntity(Company model)
            => new CompanyDto(model.CompanyId
                , model.CompanyId
                , model.Name.Value
                , model.Rnc
                , model.HomeOrApartment
                , model.City
                , model.Street
                , model.Country?.Id ?? 0
                , model.Country
                , model.Phone.Value
                , model.Website
                , model.Created
                , model.Status);
    }
}
