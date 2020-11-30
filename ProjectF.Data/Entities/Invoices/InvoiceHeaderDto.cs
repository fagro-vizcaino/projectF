using ProjectF.Data.Entities.Clients;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.PaymentList;
using System;
using System.Collections.Immutable;

namespace ProjectF.Data.Entities.Invoices
{
    public record InvoiceHeaderDto(long Id
        , string Code
        , string Ncf
        , int NumberSequenceId
        , string Rnc
        , long ClientId
        , Client Client
        , DateTime InvoiceDate
        , DateTime DueDate
        , long PaymentTermId
        , PaymentTerm PaymentTerm
        , string Notes
        , string TermAndConditions
        , string Footer
        , decimal Discount
        , decimal Subtotal
        , decimal TaxTotal
        , decimal Total
        , ImmutableList<InvoiceDetailDto> InvoiceDetails
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
