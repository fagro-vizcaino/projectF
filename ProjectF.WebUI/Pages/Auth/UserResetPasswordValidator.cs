using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.Auth
{
    public class UserResetPasswordValidator : AbstractValidator<UserResetPasswordDto>
    {
        public UserResetPasswordValidator()
        {
            RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("Contraseña Requerida")
                .MinimumLength(11)
                .WithMessage("Su contraseña debe contener mas de 11 caracteres");

            RuleFor(c => c.ConfirmPassword)
                .Equal(c => c.Password)
                .WithMessage("Ambas contraseñas deben ser iguales");
            
        }
    }
}
