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
            => (new PaymentTermDto(entity.Description.Value
                , entity.DayValue
                , entity.Created
                , entity.Modified)) with
            { Id = entity.Id, Status = entity.Status };
    }
}
