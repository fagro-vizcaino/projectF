using FluentValidation;
using ProjectF.Data.Entities.PaymentMethods;

namespace ProjectF.Application.PaymentMethods
{
    public class PaymentMethodValidator : AbstractValidator<PaymentMethodDto>
    {
        public PaymentMethodValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Description is required");
        }
    }
}
