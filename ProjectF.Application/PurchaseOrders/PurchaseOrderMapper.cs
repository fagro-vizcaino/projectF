using System.Collections.Immutable;
using System.Linq;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PurchaseOrders;

namespace ProjectF.Application.PurchaseOrders
{
    public static class PurchaseOrderMapper
    {
        public static PurchaseOrderHeader FromDto(PurchaseOrderHeaderDto dto)
            => new PurchaseOrderHeader(new Code(dto.Code)
                , new Name(dto.SupplierName)
                , dto.SupplierId
                , dto.Rnc
                , new GeneralText(dto.Address)
                , dto.DeliverDate
                , new Name(dto.PaymentTermName)
                , dto.PaymentTermId
                , new Name(dto.WarehouseName)
                , dto.WarehouseId
                , new GeneralText(dto.Notes)
                , dto.SubTotal
                , dto.TaxTotal
                , dto.DiscountTotal
                , dto.Total
                , dto.PurcharseOrderDetail.Map(c => FromDto(c)).ToImmutableList()
                , dto.Created
                , dto.Status);


        public static PurchaseOrderHeaderDto FromEntity(PurchaseOrderHeader entity)
            => new PurchaseOrderHeaderDto(entity.Id
                , entity.Code.Value
                , entity.SupplierName.Value
                , entity.SupplierId
                , entity.Rnc
                , entity.Address
                , entity.DeliverDate
                , entity.PaymentTermName.Value
                , entity.PaymentTermId
                , entity.WarehouseName.Value
                , entity.WarehouseId
                , entity.Notes
                , entity.Subtotal
                , entity.TaxTotal
                , entity.DiscountTotal
                , entity.Total
                , entity.PurchaseDetails
                    .Select(c => FromEntity(c))
                    .ToImmutableList()) 
            with { Created = entity.Created, Modified = entity.Modified, Status = entity.Status};

        public static PurchaseOrderDetail FromDto(PurchaseOrderDetailDto dto)
            => new PurchaseOrderDetail(new Code(dto.ProductCode)
                , new Name(dto.Description)
                , dto.Cost
                , dto.Qty
                , dto.DiscountValue
                , dto.TaxPercent
                  ,null);

        public static PurchaseOrderDetailDto FromEntity(PurchaseOrderDetail entity)
            => new PurchaseOrderDetailDto(entity.ProductCode.Value
                , entity.Description.Value
                , entity.Cost
                , entity.Qty
                , entity.DiscountValue
                , entity.TaxPercent) 
            with { Id = entity.Id, Created = entity.Created, Status = entity.Status, Modified = entity.Modified };
    }
}
