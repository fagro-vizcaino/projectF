using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.UnitOfMeasures
{
    public class UnitOfMeasureValidator : AbstractValidator<UnitOfMeasure>
    {
        public UnitOfMeasureValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
              .WithMessage("Nombre Requerido")
              .MaximumLength(40)
              .WithMessage("Máximo Carateres 40");

            RuleFor(c => c.Value).NotEmpty()
                .WithMessage("Valor requerido");
        }
    }
}
