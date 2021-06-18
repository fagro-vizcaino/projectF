using ProjectF.Application.PaymentTerms;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Suppliers;

namespace ProjectF.Application.Suppliers
{
    public static class SupplierMapper
    {
        public static Supplier FromDto(SupplierDto dto)
            => new(new Code(dto.Code)
                , new Name(dto.Name)
                , new Email(dto.Email)
                , new Phone(dto.Phone)
                , dto.Rnc
                , dto.HomeOrApartment
                , dto.City
                , dto.Street
                , dto.Country
                , dto.SupplierGroup
                , PaymentTermMapper.FromDto(dto.PaymentTerm)
                , new GeneralText(dto.Notes)
                , dto.IsIndependent
                , dto.Created
                , dto.Modified
                , dto.Status);

        public static SupplierDto FromEntity(Supplier entity)
            => new(entity.Code.Value
                , entity.Name.Value
                , entity.Email.Value
                , entity.Phone.Value
                , entity.Rnc
                , entity.HomeOrApartment
                , entity.City
                , entity.Street
                , entity.Country?.Id ?? 0
                , entity.Country
                , entity.IsInformalSupplier
                , entity.SupplierGroup
                , entity.PaymentTerm?.Id ?? 0
                , PaymentTermMapper.FromEntity(entity.PaymentTerm)
                , entity.Notes.Value)
            {
                Id = entity.Id, 
                Status = entity.Status,
                Created = entity.Created, 
                Modified = entity.Modified
            };
    }
}
