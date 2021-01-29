using FluentValidation;
using ProjectF.Data.Entities.PurchaseOrders;

namespace ProjectF.Application.PurchaseOrders
{
    public class PurchaseOrderValidator: AbstractValidator<PurchaseOrderHeaderDto>
    {
        public PurchaseOrderValidator()
        {
            RuleFor(c => c.SupplierName)
                .NotEmpty()
                .WithMessage("supplier name required");

            RuleFor(c => c.SupplierId)
                .NotEmpty()
                .WithMessage("supplier id required");

            RuleFor(c => c.WarehouseId)
                .NotEmpty()
                .WithMessage("warehouse id required");

            RuleFor(c => c.WarehouseName)
                .NotEmpty()
                .WithMessage("warehouse name required");

            RuleFor(c => c.SubTotal)
                .NotEmpty()
                .WithMessage("subtotal required")
                .When(c => c.PurcharseOrderDetail.Count > 0);

            RuleFor(c => c.Total)
                .NotEmpty()
                .WithMessage("total is required")
                .When(c => c.PurcharseOrderDetail.Count > 0);

            RuleForEach(c => c.PurcharseOrderDetail)
                .SetValidator(new PurchaseOrderDetailValidator());                
        }
    }

    class PurchaseOrderDetailValidator : AbstractValidator<PurchaseOrderDetailDto>
    {
        public PurchaseOrderDetailValidator()
        {
            RuleFor(c => c.ProductCode)
                .NotEmpty()
                .WithMessage("product code required");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("description required");

            RuleFor(c => c.Cost)
                .NotEmpty()
                .WithMessage("cost is required");

            RuleFor(c => c.DiscountValue)
                .GreaterThan(0)
                .When(c => c.DiscountValue != 0);

            RuleFor(c => c.Qty)
                .NotEmpty()
                .WithMessage("qty is required")
                .GreaterThan(0)
                .WithMessage("qty cannot be negative");
        }
    }
}
