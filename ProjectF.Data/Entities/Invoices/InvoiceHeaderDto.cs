using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.PaymentList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectF.Data.Entities.Invoices
{
    public class InvoiceHeaderDto
    {
        public long Id { get; }
        public string Code { get; }
        public string Ncf { get; }
        public string Rnc { get; }
        public long ClientId { get;  }
        public Client Client { get; }
        public DateTime Created { get; }
        public DateTime DueDate { get; }
        public long PaymentTermId { get;  }
        public PaymentTerm PaymentTerm { get; }
        public string Notes { get; }
        public string TermAndConditions { get; }
        public string Footer { get; }
        public decimal Discount { get; }
        public decimal SubTotal { get; }
        public decimal Total { get; }
        public decimal TaxTotal { get; }

        private readonly List<InvoiceDetailDto> _invoiceDetails = new List<InvoiceDetailDto>();
        public IReadOnlyList<InvoiceDetailDto> InvoiceDetails 
            => _invoiceDetails.ToList();

        public DateTime SystemCreated { get; }
        public DateTime? SystemModified { get; }

        public InvoiceHeaderDto(long id
            , string code
            , string ncf
            , string rnc
            , long clientId
            , Client client
            , DateTime created
            , DateTime dueDate
            , long paymentTermId
            , PaymentTerm paymentTerm
            , string notes
            , string termAndConditions
            , string footer
            , decimal discount
            , decimal subtotal
            , decimal taxTotal
            , decimal total
            , IEnumerable<InvoiceDetailDto> invoiceDetails)
        {
            Id                = id;
            Code              = code;
            Ncf               = ncf;
            Rnc               = rnc;
            Client            = client;
            ClientId          = clientId;
            Created           = created;
            DueDate           = dueDate;
            PaymentTermId     = paymentTermId;
            PaymentTerm       = paymentTerm;
            Notes             = notes;
            TermAndConditions = termAndConditions;
            Footer            = footer;
            Discount          = discount;
            SubTotal          = subtotal;
            TaxTotal          = taxTotal;
            Total             = total;
            _invoiceDetails   = invoiceDetails.ToList();
            SystemCreated     = DateTime.UtcNow;
        }

        public InvoiceHeaderDto With(long? id = null
            , string? code = null
            , string? ncf = null
            , string? rnc = null
            , Client? client = null
            , long? clientId = null
            , DateTime? created = null
            , DateTime? dueDate = null
            , PaymentTerm? paymentTerm = null
            , long? paymentTermId = null
            , string? notes = null
            , string? termAndConditions = null
            , string? footer = null
            , decimal? discount = null
            , decimal? subTotal = null
            , decimal? taxTotal = null
            , decimal? total = null
            , List<InvoiceDetailDto>? invoiceDetails = null)
        {
            return new InvoiceHeaderDto(id ?? this.Id
                , code ?? this.Code
                , ncf ?? this.Ncf
                , rnc ?? this.Rnc
                , clientId ?? this.ClientId
                , client ?? this.Client
                , created ?? this.Created
                , dueDate ?? this.DueDate
                , paymentTermId ?? this.PaymentTermId
                , paymentTerm ?? this.PaymentTerm
                , notes ?? this.Notes
                , termAndConditions ?? this.TermAndConditions
                , footer ?? this.Footer
                , discount ?? this.Discount
                , subTotal ?? this.SubTotal
                , taxTotal ?? this.TaxTotal
                , total ?? this.Total
                , invoiceDetails ?? this._invoiceDetails);
        }


        public void Deconstruct(out long id, 
            out string code,
            out string ncf, 
            out string rnc,
            out Client client,
            out long clientId,
            out DateTime created,
            out DateTime dueDate,
            out long paymentTermId,
            out PaymentTerm paymentTerm,
            out string notes,
            out string termAndConditions,
            out string footer,
            out decimal discount,
            out decimal subTotal,
            out decimal taxTotal,
            out decimal total,
            out DateTime systemCreated,
            out IEnumerable<InvoiceDetailDto> invoiceDetails)
        {
            id                 = Id;
            code               = Code;
            ncf                = Ncf;
            rnc                = Rnc;
            clientId           = ClientId;
            client             = Client;
            created            = Created;
            dueDate            = DueDate;
            paymentTermId      = PaymentTermId;
            paymentTerm        = PaymentTerm;
            notes              = Notes;
            termAndConditions  = TermAndConditions;
            footer             = Footer;
            discount           = Discount;
            subTotal           = SubTotal;
            taxTotal           = TaxTotal;
            total              = Total;
            invoiceDetails     = _invoiceDetails;
            systemCreated      = SystemCreated;

        }

    }
}
