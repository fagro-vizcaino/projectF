using System.Text.RegularExpressions;
using FluentValidation;

namespace ProjectF.WebUI.Pages.Company
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Nombre de la empresa requerido");
            RuleFor(c => c.HomeOrApartment).NotEmpty()
                .WithMessage("Requerido");
            RuleFor(c => c.Street).NotEmpty()
            .WithMessage("Requerido");
            RuleFor(c => c.Rnc).NotEmpty()
                .WithMessage("Requerido");
            RuleFor(c => c.Phone)
                .Must(c => Regex.Match(c, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$").Success);
            RuleFor(c => c.Website).NotEmpty();
            RuleFor(c => c.SelectedCountry).GreaterThan(0)
                .WithMessage("Country es requerido");
        }
    }
}
