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
                , dto.SupplierGroup
                , dto.PaymentTerm
                , new GeneralText(dto.Notes)
                , dto.IsIndependent
                , dto.Created
                , dto.Modified
                , dto.Status);

        public static SupplierDto FromEntity(Supplier entity)
            => (new SupplierDto(entity.Code.Value
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
                , entity.PaymentTerm
                , entity.Notes.Value
                , entity.Created
                , entity.Modified)) with { Id = entity.Id, Status = entity.Status };
    }
}
