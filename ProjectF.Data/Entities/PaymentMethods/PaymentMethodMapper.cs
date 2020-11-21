using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.PaymentMethods
{
    public static class PaymentMethodMapper
    {
        public static PaymentMethod FromDto(PaymentMethodDto dto)
       => new PaymentMethod(new Code(dto.Code),
           new Name(dto.Description),
           dto.Created,
           dto.Modified,
           dto.Status);

        public static PaymentMethodDto FromEntity(PaymentMethod entity)
            => new PaymentMethodDto(entity.Id, 
                entity.Code.Value, 
                entity.Description.Value, 
                entity.Created, 
                entity.Modified, 
                entity.Status);
    }
   
}
