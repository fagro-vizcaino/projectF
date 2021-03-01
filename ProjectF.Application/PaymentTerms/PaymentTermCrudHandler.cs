using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using ProjectF.Data.Entities.PaymentList;
using static ProjectF.Application.PaymentTerms.PaymentTermMapper;
using ProjectF.Application.Common;

namespace ProjectF.Application.PaymentTerms
{
    public class PaymentTermCrudHandler : BaseCrudHandler<PaymentTermDto, PaymentTerm, PaymentTermRepository>
    {
        public PaymentTermCrudHandler(PaymentTermRepository paymentTermRepository) : base(paymentTermRepository)
            => (_, _fromDto, _fromEntity, _updateEntity) 
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
