using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.PaymentList;
using static ProjectF.Data.Entities.PaymentList.PaymentTermMapper;
using ProjectF.Application.Common;

namespace ProjectF.Application.PaymentTerms
{
    public class PaymentTermCrudHandler : BaseCrudHandler<PaymentTermDto, PaymentTerm, PaymentTermRepository>
    {
        readonly PaymentTermRepository _paymentTermRepository;

        public PaymentTermCrudHandler(PaymentTermRepository paymentTermRepository) : base(paymentTermRepository)
            => (_paymentTermRepository, fromDto, fromEntity, updateEntity) 
            = (paymentTermRepository, FromDto, FromEntity, UpdateEntity);

        Either<Error, PaymentTerm> UpdateEntity(PaymentTermDto dto, PaymentTerm paymentTerm)
        {
            PaymentTerm editedPaymentTerm = FromDto(dto);
            paymentTerm.EditPaymentTerm(editedPaymentTerm.Description
                , editedPaymentTerm.DayValue
                , paymentTerm.Status);
            return paymentTerm;
        }

    }
}
