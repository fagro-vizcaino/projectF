using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using ProjectF.Data.Entities.Companies;

namespace ProjectF.Application.Companies
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Company name is required");

            RuleFor(c => c.HomeOrApartment).NotEmpty();
            RuleFor(c => c.Street).NotEmpty();
            RuleFor(c => c.Rnc).NotEmpty();
            RuleFor(c => c.Phone)
                .Must(c => Regex.Match(c, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$").Success);
            RuleFor(c => c.Website).NotEmpty();
            RuleFor(c => c.Country.Id).GreaterThan(0)
                .WithMessage("Country es requerido");

        }
    }
}
