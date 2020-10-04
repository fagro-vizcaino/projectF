using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Products;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceDetail : Entity
    {
        public Code ProductCode { get; private set; }
        public Name Description { get; private set; }
        public int Qty { get; private set; }
        public decimal Amount { get; private set; }
        public decimal TaxPercent { get; private set; }
        public virtual InvoiceHeader InvoiceHeader { get; private set; }

        protected InvoiceDetail() { }

        public InvoiceDetail(Code productCode,
            Name description,
            int qty,
            decimal amount,
            decimal taxPercent,
            InvoiceHeader invoiceHeader) : this()
        => (ProductCode, Description, Qty, Amount, TaxPercent, InvoiceHeader)
            = (productCode, description, qty, amount, taxPercent, invoiceHeader);

        public void Deconstructor(out Code productCode,
            out Name description,
            out int qty,
            out decimal amount,
            out decimal taxPercent,
            out InvoiceHeader invoiceHeader)
            => (productCode, description, qty, amount, taxPercent, invoiceHeader) 
            = (ProductCode, Description, Qty, Amount, TaxPercent, InvoiceHeader);

        public void EditInvoiceDetail(Code productCode,
            Name description,
            int qty,
            decimal amount,
            decimal taxPercent,
            InvoiceHeader invoiceHeader)
        => (ProductCode, Description, Qty, Amount, TaxPercent, InvoiceHeader)
            = (productCode, description, qty, amount, taxPercent, invoiceHeader);
    }
}
