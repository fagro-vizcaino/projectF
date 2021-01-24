using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Bills
{
    public class BillInvoiceHeader : _BaseEntity
    {
        public Code Code { get; private set; }
        public Name SupplierName { get; private set; }
        public int SupplierId { get; private set; }
        public DateTime BillDate { get; private set; }
        public DateTime BillDueDate { get; private set; }
        public Code InvoiceNumber { get; private set; }
        public Code Ncf { get; private set; }
        public string Rnc { get; private set; }
        public string PaymentTerm { get; private set; }
        public int PaymentTermId { get; private set; }
        public string WarehouseName { get; private set; }
        public int WarehouseId { get; private set; }
        public string GoodsTypeName { get; private set; }
        public int GoodsTypeId { get; private set; }
        public GeneralText Notes { get; private set; }
        public decimal Subtotal { get; private set; }
        public decimal TaxTotal { get; private set; }
        public decimal DiscountTotal { get; private set; }
        public decimal Total { get; private set; }
        
        private readonly List<BillInvoiceDetail> _billInvoiceDetails = new();
        public IReadOnlyList<BillInvoiceDetail> BillInvoiceDetails => _billInvoiceDetails.ToList();
        protected BillInvoiceHeader() { }

        public BillInvoiceHeader(Code code
            , Name supplierName
            , int supplierId
            , DateTime billDate
            , DateTime billDueDate
            , Code invoiceNumber
            , Code ncf
            , string rnc
            , string paymentTerm
            , int paymentTermId
            , string warehouseName
            , int warehouseId
            , string goodsTypeName
            , int goodsTypeId
            , GeneralText notes
            , decimal subTotal
            , decimal taxTotal
            , decimal discountTotal
            , decimal total
            , List<BillInvoiceDetail> billInvoiceDetails
            , DateTime created
            , EntityStatus status = EntityStatus.Active)
        {
            Code               = code;
            SupplierName       = supplierName;
            SupplierId         = supplierId;
            BillDate           = billDate;
            BillDueDate        = billDueDate;
            InvoiceNumber      = invoiceNumber;
            Ncf                = ncf;
            Rnc                = rnc;
            PaymentTerm        = paymentTerm;
            PaymentTermId      = paymentTermId;
            WarehouseName      = warehouseName;
            WarehouseId        = warehouseId;
            GoodsTypeName      = goodsTypeName;
            GoodsTypeId        = goodsTypeId;
            Notes              = notes;
            Subtotal           = subTotal;
            TaxTotal           = taxTotal;
            DiscountTotal      = discountTotal;
            Total              = total;
            Created            = created == DateTime.MinValue ? DateTime.UtcNow : created;
            Status             = status;

            _billInvoiceDetails.AddRange(CreateDetails(billInvoiceDetails));
        }

        private IEnumerable<BillInvoiceDetail> CreateDetails(IEnumerable<BillInvoiceDetail> billInvoiceDetails)
        {
            var billInvoiceHeader = this;
            return billInvoiceDetails.Map(d => new BillInvoiceDetail(d.ProductCode
                , d.ProductName
                , d.Amount
                , d.Qty
                , d.DiscountValue
                , d.TaxPercent
                , billInvoiceHeader));
        }

        public void EditBillInvoiceHeader(Code code
            , Name supplierName
            , int supplierId
            , DateTime billDate
            , DateTime billDueDate
            , Code invoiceNumber
            , Code ncf
            , string rnc
            , string paymentTerm
            , int paymentTermId
            , string warehouseName
            , int warehouseId
            , string goodsTypeName
            , int goodsTypeId
            , GeneralText notes
            , decimal subTotal
            , decimal taxTotal
            , decimal discountTotal
            , decimal total
            , List<BillInvoiceDetail> billInvoiceDetails)
        {
            var header = this;
            var _localBillInvoiceDetals = billInvoiceDetails.Map(d =>
                new BillInvoiceDetail(d.ProductCode
                , d.ProductName
                , d.Amount
                , d.Qty
                , d.DiscountValue
                , d.TaxPercent
                , header));

            Code          = code;
            SupplierName  = supplierName;
            SupplierId    = supplierId;
            Rnc           = rnc;
            Ncf           = ncf;
            InvoiceNumber = invoiceNumber;
            BillDate      = billDate;
            BillDueDate   = billDueDate;
            PaymentTerm   = paymentTerm;
            PaymentTermId = paymentTermId;
            WarehouseName = warehouseName;
            WarehouseId   = warehouseId;
            GoodsTypeName = goodsTypeName;
            GoodsTypeId   = goodsTypeId;
            Notes         = notes;
            Subtotal      = subTotal;
            TaxTotal      = taxTotal;
            DiscountTotal = discountTotal;
            Total         = total;
            Status        = EntityStatus.Active;
            Modified      = DateTime.UtcNow;
            _billInvoiceDetails.AddRange(_localBillInvoiceDetals);
        }
    }
}
