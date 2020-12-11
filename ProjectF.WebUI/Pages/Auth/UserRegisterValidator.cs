using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.Auth
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty()
              .WithMessage("Nombre Requerido")
              .MaximumLength(40)
              .WithMessage("Máximo Carateres 40");

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Apellido Requerido")
                .MaximumLength(40)
                .WithMessage("Máximo Carateres 40");

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
