using FluentValidation;
using ProjectF.Data.Entities.UnitOfMeasures;

namespace ProjectF.Application.UnitOfMeasures
{
    public class UnitOfMeasureValidator : AbstractValidator<UnitOfMeasureDto>
    {
        public UnitOfMeasureValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");

            RuleFor(c => c.Value)
                .NotEmpty()
                .WithMessage("value is required");
        }
    }
}
