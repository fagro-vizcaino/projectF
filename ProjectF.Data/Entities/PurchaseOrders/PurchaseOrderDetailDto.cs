using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.PurchaseOrders
{
    public record PurchaseOrderDetailDto(string ProductCode
        , string Description
        , decimal Cost
        , decimal Qty
        , decimal DiscountValue
        , decimal TaxPercent): FDto;
}
