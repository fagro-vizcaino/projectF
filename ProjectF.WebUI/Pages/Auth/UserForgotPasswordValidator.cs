using FluentValidation;

namespace ProjectF.WebUI.Pages.Auth
{
    public class UserForgotPasswordValidator : AbstractValidator<UserForgotPasswordDto>
    {
        public UserForgotPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                   .WithMessage("Email Requerido")
                   .MaximumLength(40)
                   .WithMessage("Máximo Carateres 40")
                   .EmailAddress()
                   .WithMessage("Email no es valido");
        }
    }

}
