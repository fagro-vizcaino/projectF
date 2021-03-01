using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentMethods;

namespace ProjectF.Application.PaymentMethods
{
    public static class PaymentMethodMapper
    {
        public static PaymentMethod FromDto(PaymentMethodDto dto)
       => new(new Code(dto.Code),
           new Name(dto.Description),
           dto.Created,
           dto.Status);

        public static PaymentMethodDto FromEntity(PaymentMethod entity)
            => new(entity.Code.Value, 
                entity.Description.Value, 
                entity.Created, 
                entity.Modified)
            {
                Id = entity.Id, 
                Status = entity.Status,
                Modified = entity.Modified
            };
    }
}
