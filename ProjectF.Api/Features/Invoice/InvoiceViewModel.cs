
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Entities.Clients;
using static ProjectF.Data.Entities.Clients.ClientMapper;
using ProjectF.Application.Invoice;
using ProjectF.Data.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Api.Features.ContactClient;
using ProjectF.Api.Features.PaymentTerms;
using ProjectF.Data.Entities.Common;
using System.Collections.Immutable;

namespace ProjectF.Api.Features.Invoice
{
    public class InvoiceViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Ncf { get; set; }
        public int NumberSequenceId { get; set;}
        public string Rnc { get; set; }
        public ClientDto Client { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public PaymentTermDto PaymentTerm { get; set; }
        public string Notes { get; set; }
        public string TermAndConditions { get; set; }
        public string Footer { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetailViewModel> InvoiceDetails { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }
        public InvoiceHeaderDto ToDto()
        {
            var client = new Client(new Code(Client.Code),
                new Name(Client.Firstname),
                new Name(Client.Lastname),
                new Email(Client.Email),
                new Phone(Client.Phone),
                Client.Birthday,
                Client.Rnc,
                Client.HomeOrApartment,
                Client.City,
                Client.Street,
                new Country(Client.SelectedCountry, "", ""),
                Client.Created,
                Client.Status);

            var paymentTerm = new PaymentTerm(new Name(PaymentTerm.Description),
                PaymentTerm.DayValue,
                PaymentTerm.Created,
                PaymentTerm.Status);

            var details = InvoiceDetails.Map(i => new InvoiceDetailDto(
                    i.Id,
                    i.ProductCode,
                    i.ProductDescription,
                    i.Qty,
                    i.Amount,
                    i.TaxPercent));


            return new InvoiceHeaderDto(Id
                , Code
                , Ncf
                , NumberSequenceId
                , Rnc
                , Client.Id
                , client
                , Created
                , DueDate
                , PaymentTerm.Id
                , paymentTerm
                , Notes
                , TermAndConditions
                , Footer
                , Discount
                , SubTotal
                , TaxTotal
                , Total
                , details.ToImmutableList()
                , Created
                , Modified
                , Status);
        }
        public static InvoiceViewModel FromDtoToView(InvoiceHeaderDto invoiceDto)
        {
            
                
            var paymentTerm = (new PaymentTermDto(
                invoiceDto.PaymentTerm.Description.Value,
                invoiceDto.PaymentTerm.DayValue,
                invoiceDto.PaymentTerm.Created,
                invoiceDto.PaymentTerm.Modified)) 
                with { Id = invoiceDto.PaymentTerm.Id, Status  = invoiceDto.PaymentTerm.Status };

            return new InvoiceViewModel()
            {
                Id                = invoiceDto.Id,
                Client            = FromEntity(invoiceDto.Client),
                Code              = invoiceDto.Code,
                Rnc               = invoiceDto.Rnc,
                Ncf               = invoiceDto.Ncf,
                Created           = invoiceDto.InvoiceDate,
                DueDate           = invoiceDto.DueDate,
                Footer            = invoiceDto.Footer,
                Notes             = invoiceDto.Notes,
                PaymentTerm       = paymentTerm,
                TermAndConditions = invoiceDto.TermAndConditions,
                Discount          = invoiceDto.Discount,
                SubTotal          = invoiceDto.Subtotal,
                TaxTotal          = invoiceDto.TaxTotal,
                Total             = invoiceDto.Total,
                InvoiceDetails    = invoiceDto.InvoiceDetails.Map(i => new InvoiceDetailViewModel()
                {
                    Id                 = i.Id,
                    ProductCode        = i.ProductCode,
                    ProductDescription = i.ProductDescription,
                    Amount             = i.Amount,
                    Qty                = i.Qty,
                    TaxPercent         = i.TaxPercent
                }).ToList()

            };
        }
    }
}


