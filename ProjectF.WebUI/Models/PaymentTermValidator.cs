using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class PaymentTermValidator : AbstractValidator<PaymentTerm>
    {
        public PaymentTermValidator()
        {
            RuleFor(c => c.Description).NotEmpty()
                .WithMessage("Nombre Requerido")
                .MaximumLength(40)
                .WithMessage("Máximo Carateres 40");

            RuleFor(c => c.DayValue)
                .LessThan(9999)
                .WithMessage("Máximo de dias excedido");
        }
    }
}
