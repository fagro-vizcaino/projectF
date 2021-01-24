using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Bills
{
    public class BillInvoiceDetail : _BaseEntity
    {
        public Code ProductCode { get; private set; }
        public Name ProductName { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Qty { get; private set; }
        public decimal DiscountValue { get; private set; }
        public decimal TaxPercent { get; private set; }
        public BillInvoiceHeader BillInvoiceHeader { get; private set; }

        protected BillInvoiceDetail() { }

        public BillInvoiceDetail(Code productCode
            , Name productName
            , decimal amount
            , decimal qty
            , decimal discountValue
            , decimal taxPercent
            , BillInvoiceHeader billInvoiceHeader)
        {
            ProductCode       = productCode;
            ProductName       = productName;
            Amount            = amount;
            Qty               = qty;
            DiscountValue     = discountValue;
            TaxPercent        = taxPercent;
            BillInvoiceHeader = billInvoiceHeader;
        }

        public void Deconstruct(out Code dproductCode
            , out Name dproductName
            , out decimal dqty
            , out decimal damount
            , out decimal dtaxpercent
            , out decimal ddiscountValue
            , out BillInvoiceHeader dbillInvoiceHeader)
        {
            dproductCode       = ProductCode;
            dproductName       = ProductName;
            ddiscountValue     = DiscountValue;
            dqty               = Qty;
            damount            = Amount;
            dtaxpercent        = TaxPercent;
            dbillInvoiceHeader = BillInvoiceHeader;
        }
    }
}
