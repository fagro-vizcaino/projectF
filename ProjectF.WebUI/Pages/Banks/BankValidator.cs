using FluentValidation;

namespace ProjectF.WebUI.Pages
{
    public class BankValidator : AbstractValidator<Bank>
    {
        public BankValidator()
        {
            RuleFor(c => c.AccountName).NotEmpty()
                .WithMessage("Nombre de la cuenta requerido")
                .MaximumLength(40)
                .WithMessage("Máximo Carateres (40) excedido");


            RuleFor(c => c.BankAccountTypeId).NotEmpty()
                .WithMessage("Tipo de cuenta requerido");

        }
    }
}
