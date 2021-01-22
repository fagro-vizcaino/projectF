using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceHeader : _BaseEntity
    {
        public Code Code { get; private set; }
        public string Ncf { get; private set; }
        public int NumberSequenceId { get; private set; }
        public string Rnc { get; private set; }
        public virtual Client Client { get; private set; }
        //public virtual Company Company { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public PaymentTerm PaymentTerm { get; private set; }
        public decimal Discount { get; private set; }
        public decimal SubTotal { get; private set; }
        public decimal TaxTotal { get; private set; }
        public decimal Total { get; private set; }
        public GeneralText Notes { get; private set; }
        public GeneralText TermAndConditions { get; private set; }
        public GeneralText Footer { get; private set; }

        private readonly List<InvoiceDetail> _invoiceDetails = new List<InvoiceDetail>();
        public virtual IReadOnlyList<InvoiceDetail> InvoiceDetails => _invoiceDetails.ToList();

        protected InvoiceHeader() { }

        public InvoiceHeader(Code code,
            string ncf,
            int numberSequenceId,
            string rnc,
            Client client,
            DateTime invoiceDate,
            DateTime dueDate,
            PaymentTerm paymentTerm,
            GeneralText notes,
            GeneralText termAndConditions,
            GeneralText footer,
            decimal discount,
            decimal subTotal,
            decimal taxTotal,
            decimal total,
            List<InvoiceDetail> invoiceDetail,
            DateTime created,
            EntityStatus status = EntityStatus.Active)
        {
            Code              = code;
            Ncf               = ncf;
            NumberSequenceId  = numberSequenceId;
            Rnc               = rnc;
            Client            = client;
            InvoiceDate       = invoiceDate;
            DueDate           = dueDate;
            PaymentTerm       = paymentTerm;
            Notes             = notes;
            TermAndConditions = termAndConditions;
            Footer            = footer;
            Discount          = discount;
            SubTotal          = subTotal;
            TaxTotal          = taxTotal;
            Total             = total;
            Created           = created == DateTime.MinValue ? DateTime.Now : created;
            Status            = status;
            _invoiceDetails.AddRange(CreateDetails(invoiceDetail));
        }


        IEnumerable<InvoiceDetail> CreateDetails(IEnumerable<InvoiceDetail> details)
        {
            var invoiceHeader = this;
            return details.Map(d => new InvoiceDetail(d.ProductCode
                , d.Description
                , d.Qty
                , d.Amount
                , d.TaxPercent
                , invoiceHeader));
        }

        public void EditInvoiceHeader(Code code,
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
            Rnc                         = rnc;
            Client                      = client;
            DueDate                     = dueDate;
            PaymentTerm                 = paymentTerm;
            Notes                       = notes;
            TermAndConditions           = termAndConditions;
            Footer                      = footer;
            Modified                    = DateTime.UtcNow;
            _invoiceDetails.RemoveAll(c => c.Id == c.Id);
            _invoiceDetails.AddRange(_localInvoiceDetails);
        }
    }
}
