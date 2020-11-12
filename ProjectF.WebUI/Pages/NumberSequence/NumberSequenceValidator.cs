using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectF.WebUI.Pages.NumberSequence
{
    public class NumberSequenceValidator : AbstractValidator<NumberSequence>
    {
        public NumberSequenceValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty()
                .WithMessage("Nombre requerido")
                .MaximumLength(60)
                .WithMessage("Máximo Carateres 60");
            
            RuleFor(n => n.Prefix)
                .NotEmpty()
                .WithMessage("Prefijo requerido")
                .MaximumLength(5)
                .WithMessage("Máximo Carateres 5");

            RuleFor(n => n.InitialSequence)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Sequencia Inicial debe ser mayor a 0")
                .LessThan(n => n.FinalSequence)
                .WithMessage("Sequencia Inicial debe ser menor a Sequencia final");

            RuleFor(n => n.FinalSequence)
                .NotEmpty()
                .WithMessage("Sequencia inicial requerido")
                .GreaterThan(n => n.InitialSequence)
                .WithMessage("Sequencia final debe ser mayor a Sequencia Inicial");
        }
    }
}
