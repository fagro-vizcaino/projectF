using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;

namespace ProjectF.Application.PaymentTerms
{
    public static class PaymentTermMapper
    {
        public static PaymentTerm FromDto(PaymentTermDto dto)
            => new(new Name(dto.Description)
                , dto.DayValue
                , dto.Created
                , dto.Status);

        public static PaymentTermDto FromEntity(PaymentTerm entity)
            => new(entity.Description.Value, entity.DayValue)
            {
                Id = entity.Id, 
                Status = entity.Status, 
                Created = entity.Created, 
                Modified = entity.Modified
            };
    }
}
