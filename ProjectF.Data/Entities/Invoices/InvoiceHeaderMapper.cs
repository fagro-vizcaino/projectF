using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Invoices
{
    public static class InvoiceHeaderMapper
    {
        public static InvoiceHeader FromDto(InvoiceHeaderDto dto)
            => new InvoiceHeader(new Code(dto.Code)
                , dto.Ncf
                , dto.NumberSequenceId
                , dto.Rnc
                , dto.Client
                , dto.InvoiceDate
                , dto.DueDate
                , dto.PaymentTerm
                , new GeneralText(dto.Notes)
                , new GeneralText(dto.TermAndConditions)
                , new GeneralText(dto.Footer)
                , dto.Discount
                , dto.Subtotal
                , dto.TaxTotal
                , dto.Total
                , dto.InvoiceDetails.Select(c => FromDto(c)).ToList()
                , dto.Created
                , dto.Status);

        public static InvoiceHeaderDto FromEntity(InvoiceHeader entity)
            => new InvoiceHeaderDto(entity.Id
                , entity.Code.Value
                , entity.Ncf
                , entity.NumberSequenceId
                , entity.Rnc
                , entity.Client.Id
                , entity.Client
                , entity.InvoiceDate
                , entity.DueDate
                , entity.PaymentTerm.Id
                , entity.PaymentTerm
                , entity.Notes.Value
                , entity.TermAndConditions.Value
                , entity.Footer.Value
                , entity.Discount
                , entity.SubTotal
                , entity.TaxTotal
                , entity.Total
                , entity.InvoiceDetails.Select(c => FromEntity(c)).ToImmutableList()
                , entity.Created
                , entity.Modified
                , entity.Status);

        public static InvoiceDetail FromDto(InvoiceDetailDto dto)
            => new InvoiceDetail(new Code(dto.ProductCode)
                , new Name(dto.ProductDescription)
                , dto.Qty
                , dto.Amount
                , dto.TaxPercent
                , null);

        public static InvoiceDetailDto FromEntity(InvoiceDetail entity)
            => new InvoiceDetailDto(entity.Id
                , entity.ProductCode.Value
                , entity.Description.Value
                , entity.Qty
                , entity.Amount
                , entity.TaxPercent);
    }
}
