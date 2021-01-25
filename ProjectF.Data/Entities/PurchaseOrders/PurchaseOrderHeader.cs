using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;

namespace ProjectF.Data.Entities.PurchaseOrders
{
    public class PurchaseOrderHeader : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name SupplierName { get; private set; }
        public int SupplierId { get; private set; }
        public string Rnc { get; private set; }
        public GeneralText Address { get; private set; }
        public DateTime DeliverDate { get; private set; }
        public Name PaymentTermName { get; private set; }
        public int PaymentTermId { get; private set; }
        public Name WarehouseName { get; private set; }
        public int WarehouseId { get; private set; }
        public GeneralText Notes { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal TaxTotal { get; private set; }
        public decimal DiscountTotal { get; private set; }
        public decimal Total { get; private set; }

        private readonly List<PurchaseOrderDetail> _purchaseOrderDetails = new List<PurchaseOrderDetail>();
        public  IReadOnlyList<PurchaseOrderDetail> PurchaseDetails => _purchaseOrderDetails.ToList();
        
        protected PurchaseOrderHeader() { }

        public PurchaseOrderHeader(Code code
            , Name supplierName
            , int supplierId
            , string rnc
            , GeneralText address
            , DateTime deliverDate
            , Name paymentTermName
            , int paymentTermId
            , Name warehouseName
            , int warehouseId
            , GeneralText note
            , decimal subtotal
            , decimal taxTotal
            , decimal discountTotal
            , decimal total
            , List<PurchaseOrderDetail> purchaseOrderDetails
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Code            = code;
            SupplierName    = supplierName;
            SupplierId      = supplierId;
            Rnc             = rnc;
            Address         = address;
            DeliverDate     = deliverDate;
            PaymentTermName = paymentTermName;
            PaymentTermId   = paymentTermId;
            WarehouseName   = warehouseName;
            WarehouseId     = warehouseId;
            Notes           = note;
            Subtotal        = subtotal;
            TaxTotal        = taxTotal;
            DiscountTotal   = discountTotal;
            Total           = total;
            Status          = status;
            Created         = created == DateTime.MinValue ? DateTime.UtcNow : created;
            _purchaseOrderDetails.AddRange(CreateDetails(purchaseOrderDetails));
        }

        IEnumerable<PurchaseOrderDetail> CreateDetails(IEnumerable<PurchaseOrderDetail> purchaseOrderDetails)
        {
            var purchaseOrderHeader = this;
            return purchaseOrderDetails.Map(d => new PurchaseOrderDetail(d.ProductCode
                , d.Description
                , d.DiscountValue
                , d.Qty
                , d.Cost
                , d.TaxPercent
                , purchaseOrderHeader));
        }

        public void EditPurchaseOrderHeader(Code code
            , Name supplierName
            , int supplierId
            , string rnc
            , GeneralText address
            , DateTime deliverDate
            , Name paymentTermName
            , int paymentTermId
            , Name warehouseName
            , int warehouseId
            , GeneralText note
            , decimal subtotal
            , decimal taxTotal
            , decimal discountTotal
            , decimal total
            , List<PurchaseOrderDetail> purchaseOrderDetails)
        {
            var header = this;
            var _localpurchaseOrder = purchaseOrderDetails.Map(d =>
                new PurchaseOrderDetail(d.ProductCode
                , d.Description
                , d.Cost
                , d.Qty
                , d.DiscountValue
                , d.TaxPercent
                , header));

            Code            = code;
            SupplierName    = supplierName;
            SupplierId      = supplierId;
            Rnc             = rnc;
            Address         = address;
            DeliverDate     = deliverDate;
            PaymentTermName = paymentTermName;
            PaymentTermId   = paymentTermId;
            WarehouseName   = warehouseName;
            WarehouseId     = warehouseId;
            Notes           = note;
            Subtotal        = subtotal;
            TaxTotal        = taxTotal;
            DiscountTotal   = discountTotal;
            Total           = total;
            Status          = EntityStatus.Active;
            Modified        = DateTime.UtcNow;
            _purchaseOrderDetails.AddRange(_localpurchaseOrder);
        }
    }
}
