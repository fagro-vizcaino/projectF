using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class WerehouseValidator : AbstractValidator<Werehouse>
    {
        public WerehouseValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
              .WithMessage("Nombre Requerido")
              .MaximumLength(40)
              .WithMessage("Máximo Carateres 40");

            RuleFor(x => x.Location)
                .MaximumLength(80)
                .WithMessage("Máximo Carateres 80");
        }
    }
}
