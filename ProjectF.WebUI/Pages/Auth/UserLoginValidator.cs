using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.Auth
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        const int MIN_PASSWORD_LENGTH = 10;
        const int MAX_PASSWORD_LENGTH = 60;
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
               .WithMessage("Email Requerido")
               .MaximumLength(40)
               .WithMessage("Máximo Carateres 40")
               .EmailAddress()
               .WithMessage("Email no es valido");

            RuleFor(x => x.Password).NotEmpty()
             .WithMessage("Contraseña Requerido")
             .MaximumLength(MAX_PASSWORD_LENGTH)
             .WithMessage($"Máximo Carateres {MAX_PASSWORD_LENGTH}")
             .MinimumLength(MIN_PASSWORD_LENGTH)
             .WithMessage($"Su contraseña debe tener mas de {MIN_PASSWORD_LENGTH} caracteres");
        }
    }
}
