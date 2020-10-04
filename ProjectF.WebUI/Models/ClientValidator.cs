using FluentValidation;

namespace ProjectF.WebUI.Models
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("Nombre es requerido");
        }
    }
}
