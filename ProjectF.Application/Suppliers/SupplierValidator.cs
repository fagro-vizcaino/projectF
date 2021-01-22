using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProjectF.Data.Entities.Suppliers;

namespace ProjectF.Application.Suppliers
{
    public class SupplierValidator : AbstractValidator<SupplierDto>
    {
        public SupplierValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(c => c.PaymentTermId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("PaymentTermId wrong value");

            RuleFor(c => c.SupplierGroup)
                .NotEmpty()
                .WithMessage("supplierGroud is required");

            RuleFor(c => c.SelectedCountry)
                .NotEmpty()
                .WithMessage("country is required");
                
        }
    }
}
