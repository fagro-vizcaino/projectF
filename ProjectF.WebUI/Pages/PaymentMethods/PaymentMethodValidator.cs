using FluentValidation;

namespace ProjectF.WebUI.Pages.PaymentMethods
{
    public class PaymentMethodValidator : AbstractValidator<PaymentMethod>
    {
        public PaymentMethodValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Descripción Requerido")
                .MaximumLength(40)
                .WithMessage("Máximo Carateres 40");
        }
    }
}
