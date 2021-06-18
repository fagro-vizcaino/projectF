using FluentValidation;
using ProjectF.Data.Entities.Taxes;

namespace ProjectF.Application.Taxes
{
    public class TaxValidator : AbstractValidator<TaxDto>
    {
        public TaxValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(c => c.PercentValue)
                .NotEmpty()
                .WithMessage("Percent value is required");
        }
    }
}
