using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(s => s.Name).NotEmpty()
                .WithMessage("Nombre Requerido")
                .MaximumLength(44)
                .WithMessage("Máximo Carateres (40) excedido");
        }
    }
}
