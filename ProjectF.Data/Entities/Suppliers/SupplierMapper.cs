using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Suppliers
{
    public static class SupplierMapper
    {
        public static Supplier FromDto(SupplierDto dto)
            => new Supplier(new Code(dto.Code)
                , new Name(dto.Name)
                , new Email(dto.Email)
                , new Phone(dto.Phone)
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , dto.IsIndependent
                , dto.Created
                , dto.Modified
                , dto.Status);

        public static SupplierDto FromEntity(Supplier entity)
            => new SupplierDto(entity.Id
                , entity.Code.Value
                , entity.Name.Value
                , entity.Email.Value
                , entity.Phone.Value
                , entity.Rnc
                , entity.HomeOrApartment
                , entity.City
                , entity.Street
                , entity.Country?.Id ?? 0
                , entity.Country
                , entity.IsIndependent
                , entity.Created
                , entity.Modified
                , entity.Status);
    }
}
