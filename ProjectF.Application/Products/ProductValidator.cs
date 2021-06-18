using FluentValidation;
using ProjectF.Data.Entities.Products;

namespace ProjectF.Application.Products
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }

    public class ProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(c => c.Code)
                .NotEmpty()
                .WithMessage("Code is required");

            RuleFor(c => c.Name)
              .NotEmpty()
              .WithMessage("Name is required");

            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .WithMessage("Category id is required");

            RuleFor(c => c.WarehouseId)
                .NotEmpty()
                .WithMessage("Warehouse id is required");

            RuleFor(c => c.UnitOfMeasureId)
                .NotEmpty()
                .WithMessage("Unit of measure id is required");

        }
    }
}
