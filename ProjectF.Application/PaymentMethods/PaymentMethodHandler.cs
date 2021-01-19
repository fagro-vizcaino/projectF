using ProjectF.Data.Entities.PaymentMethods;
using ProjectF.Data.Repositories;
using ProjectF.Application.Common;
using static ProjectF.Data.Entities.PaymentMethods.PaymentMethodMapper;
namespace ProjectF.Application.PaymentMethods
{
    public class PaymentMethodHandler : BaseCrudHandler<PaymentMethodDto, PaymentMethod, PaymentMethodRepository>
    {
        readonly PaymentMethodRepository _repo;

        public PaymentMethodHandler(PaymentMethodRepository paymentMethodRepository) : base(paymentMethodRepository)
            => (_repo, fromDto, fromEntity ) = (paymentMethodRepository, FromDto, FromEntity);

        
    }
}
