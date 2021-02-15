using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PaymentList
{
    public static class PaymentTermMapper
    {
        public static PaymentTerm FromDto(PaymentTermDto dto)
            => new PaymentTerm(new Name(dto.Description)
                , dto.DayValue
                , dto.Created
                , dto.Status);

        public static PaymentTermDto FromEntity(PaymentTerm entity)
            => (new PaymentTermDto(entity.Description.Value, entity.DayValue)) 
            with {
                    Id = entity.Id
                    , Status = entity.Status
                    , Created = entity.Created
                    , Modified = entity.Modified
                };
    }
}
