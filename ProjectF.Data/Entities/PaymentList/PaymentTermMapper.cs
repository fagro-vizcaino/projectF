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
            => new PaymentTermDto(entity.Id
                , entity.Description.Value
                , entity.DayValue
                , entity.Created
                , entity.Modified
                , entity.Status);
    }
}
