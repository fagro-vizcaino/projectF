using FluentValidation;

namespace ProjectF.WebUI.Models
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty()
                .WithMessage("Nombre es requerido");
        }
    }
}
