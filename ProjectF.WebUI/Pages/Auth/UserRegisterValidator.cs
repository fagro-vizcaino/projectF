using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.Auth
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        const int MIN_PASSWORD_LENGHT = 11;
        const int MAX_PASSWORD_LENGHT = 60;
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
              .MaximumLength(MAX_PASSWORD_LENGHT)
              .WithMessage($"Máximo Carateres {MAX_PASSWORD_LENGHT}")
              .MinimumLength(MIN_PASSWORD_LENGHT)
              .WithMessage($"Su contraseña debe tener mas de {MIN_PASSWORD_LENGHT} caracteres");
        }
    }
}
