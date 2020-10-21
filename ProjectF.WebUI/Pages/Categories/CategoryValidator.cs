using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Models
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Nombre Requerido")
                .MaximumLength(40)
                .WithMessage("Máximo Carateres (40) excedido");
        }
    } 
}
