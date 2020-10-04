using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Products;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceHeader : Entity
    {
        public Code Code { get; private set; }
        public NumberSequence Ncf { get; private set; }
        public string NcfType { get; private set; }
        public string Rnc { get; private set; }
        public virtual Client Client { get; private set; }
        public virtual Company Company { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime DueDate { get; private set; }

        public virtual PaymentTerm PaymentTerm { get; private set; }
        public decimal Discount { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal TaxTotal { get; private set; }
        public decimal Total { get; private set; }
        public GeneralText Notes { get; private set; }
        public GeneralText TermAndConditions { get; private set; }
        public GeneralText Footer { get; private set; }

        private readonly List<InvoiceDetail> _invoiceDetails = new List<InvoiceDetail>();
        public virtual IReadOnlyList<InvoiceDetail> InvoiceDetails => _invoiceDetails.ToList();

        public DateTime SystemCreated { get; private set; }
        public DateTime? SystemModified { get; private set; }

        protected InvoiceHeader() { }

        public InvoiceHeader(Code code,
            NumberSequence ncf,
            string ncfType,
            string rnc,
            Client client,
            DateTime created,
            DateTime dueDate,
            PaymentTerm paymentTerm,
            GeneralText notes,
            GeneralText termAndConditions,
            GeneralText footer,
            decimal discount,
            decimal subTotal,
            decimal taxTotal,
            decimal total,
            List<InvoiceDetailDto> invoiceDetail)
        {
            Code              = code;
            Ncf               = ncf;
            NcfType           = ncfType;
            Rnc               = rnc;
            Client            = client;
            Created           = created;
            DueDate           = dueDate;
            PaymentTerm       = paymentTerm;
            Notes             = notes;
            TermAndConditions = termAndConditions;
            Footer            = footer;
            Discount          = discount;
            SubTotal          = subTotal;
            TaxTotal          = taxTotal;
            Total             = total;
            SystemCreated     = DateTime.UtcNow;
            _invoiceDetails.AddRange(CreateDetails(invoiceDetail));
        }


        IEnumerable<InvoiceDetail> CreateDetails(IEnumerable<InvoiceDetailDto> details)
        {
            var invoiceHeader = this;
            return details.Map(d => new InvoiceDetail(
                new Code(d.ProductCode),
                new Name(d.ProductDescription),
                d.Qty,
                d.Amount,
                d.TaxPercent,
                invoiceHeader));
        }

        public void EditInvoiceHeader(Code code,
            NumberSequence ncf,
            string ncfType,
            string rnc,
            Client client,
            DateTime dueDate,
            PaymentTerm paymentTerm,
            GeneralText notes,
            GeneralText termAndConditions,
            GeneralText footer,
            IEnumerable<InvoiceDetailDto> invoiceDetails)
        {
            var invoiceHeader = this;
            var _localInvoiceDetails = invoiceDetails.Map(d => 
                new InvoiceDetail(new Code(d.ProductCode),
                    new Name(d.ProductDescription),
                    d.Qty,
                    d.Amount,
                    d.TaxPercent,
                    invoiceHeader));

            Code                        = code;
            Ncf                         = ncf;
            NcfType                     = ncfType;
            Rnc                         = rnc;
            Client                      = client;
            DueDate                     = dueDate;
            PaymentTerm                 = paymentTerm;
            Notes                       = notes;
            TermAndConditions           = termAndConditions;
            Footer                      = footer;
            SystemModified              = DateTime.UtcNow;
            _invoiceDetails.RemoveAll(c => c.Id == c.Id);
            _invoiceDetails.AddRange(_localInvoiceDetails);
        }
    }
}
