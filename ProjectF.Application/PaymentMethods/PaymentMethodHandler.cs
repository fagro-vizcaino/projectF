using ProjectF.Data.Entities.PaymentMethods;
using ProjectF.Data.Repositories;
using ProjectF.Application.Common;
using static ProjectF.Application.PaymentMethods.PaymentMethodMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using LanguageExt.Common;
using LanguageExt;

namespace ProjectF.Application.PaymentMethods
{
    public class PaymentMethodHandler : BaseCrudHandler<PaymentMethodDto, PaymentMethod, PaymentMethodRepository>
    {
        public PaymentMethodHandler(PaymentMethodRepository paymentMethodRepository) : base(paymentMethodRepository)
            => (_, _fromDto, _fromEntity, _updateEntity ) 
            = (paymentMethodRepository, FromDto, FromEntity, UpdateEntity);

        Either<Error, PaymentMethod> UpdateEntity(PaymentMethodDto dto, PaymentMethod entity)
        {
            var name = new Name(dto.Description);
            entity.EditPaymentMethod(name, entity.Status);
            return entity;
        }

    }
}
