using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Nombre es requerido")
                .MaximumLength(60)
                .WithMessage("Máximo Carateres (60) excedido");
        }
    }
}
