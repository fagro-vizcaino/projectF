using System;
using System.Collections.Immutable;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.PurchaseOrders
{
    public record PurchaseOrderHeaderDto(int Id
        , string Code
        , string SupplierName
        , int SupplierId
        , string Rnc
        , string Address
        , DateTime DeliverDate
        , string PaymentTermName
        , int PaymentTermId
        , string WarehouseName
        , int WarehouseId
        , string Notes
        , decimal SubTotal
        , decimal TaxTotal
        , decimal DiscountTotal
        , decimal Total
        , ImmutableList<PurchaseOrderDetailDto> PurcharseOrderDetail) : FDto;
}
