using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.Auth
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
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
             .MaximumLength(60)
             .WithMessage("Máximo Carateres 40")
             .MinimumLength(11)
             .WithMessage("Su contraseña debe tener mas de 4 caracteres");
        }
    }
}
