using ProjectF.Data.Entities.PaymentMethods;
using ProjectF.Data.Repositories;
using ProjectF.Application.Common;
using static ProjectF.Data.Entities.PaymentMethods.PaymentMethodMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using LanguageExt.Common;
using LanguageExt;

namespace ProjectF.Application.PaymentMethods
{
    public class PaymentMethodHandler : BaseCrudHandler<PaymentMethodDto, PaymentMethod, PaymentMethodRepository>
    {
        readonly PaymentMethodRepository _repo;

        public PaymentMethodHandler(PaymentMethodRepository paymentMethodRepository) : base(paymentMethodRepository)
            => (_repo, fromDto, fromEntity, updateEntity ) 
            = (paymentMethodRepository, FromDto, FromEntity, UpdateEntity);

        Either<Error, PaymentMethod> UpdateEntity(PaymentMethodDto dto, PaymentMethod entity)
        {
            var name = new Name(dto.Description);
            entity.EditPaymentMethod(name, entity.Status);
            return entity;
        }

    }
}
