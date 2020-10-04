using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class TaxValidator : AbstractValidator<Tax>
    {
        public TaxValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
              .WithMessage("Nombre Requerido")
              .MaximumLength(40)
              .WithMessage("Máximo Carateres 40");

            RuleFor(x => x.Percentvalue);
        }
    }
}
