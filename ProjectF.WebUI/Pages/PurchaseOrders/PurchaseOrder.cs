using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ProjectF.WebUI.Models;

namespace ProjectF.WebUI.Pages.PurchaseOrders
{
    public class PurchaseOrder : FEntity
    {
        public string Code { get; set; }
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }
        public string Rnc { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public DateTime DeliverDate { get; set; }
        public string PaymentTermName { get; set; }
        public int PaymentTermId { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseId { get; set; }
        public string Notes { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal Total { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }

    public class PurchaseOrderDetail : FEntity
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal Qty { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TaxPercent { get; set; }
    }
}
