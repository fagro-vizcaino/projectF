
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Entities.Clients;
using ProjectF.Application.Invoice;
using ProjectF.Data.Entities.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.Api.Features.ContactClient;
using ProjectF.Api.Features.PaymentTerms;

namespace ProjectF.Api.Features.Invoice
{
    public class InvoiceViewModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Ncf { get; set; }
        public string Rnc { get; set; }
        public ClientViewModel Client { get; set; }
        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }
        public PaymentTermViewModel PaymentTerm { get; set; }
        public string Notes { get; set; }
        public string TermAndConditions { get; set; }
        public string Footer { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public List<InvoiceDetailViewModel> InvoiceDetails { get; set; }
        public InvoiceHeaderDto ToDto()
        {
            var client = new Client(new Code(Client.Code),
                new Name(Client.Name),
                new Email(Client.Email),
                new Phone(Client.Phone),
                Client.Birthday,
                Client.Rnc,
                Client.HomeOrApartment,
                Client.City,
                Client.Street,
                new Country(Client.SelectedCountry, "", ""));

            var paymentTerm = new PaymentTerm(new Name(PaymentTerm.Description),
                PaymentTerm.DayValue);

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
                , details);
        }
        public static InvoiceViewModel FromDto(InvoiceHeaderDto invoiceDto)
        {
            var client = new ClientViewModel()
            {
                Id              = invoiceDto.Client.Id,
                Code            = invoiceDto.Client.Code.Value,
                Name            = invoiceDto.Client.Name.Value,
                Rnc             = invoiceDto.Client.Rnc,
                City            = invoiceDto.Client.City,
                Email           = invoiceDto.Client.Email,
                HomeOrApartment = invoiceDto.Client.HomeOrApartment,
                Phone           = invoiceDto.Client.Phone.Value,
                SelectedCountry = invoiceDto.Client.Country?.Id ?? 0,
                Street          = invoiceDto.Client.Street
            };

            var paymentTerm = new PaymentTermViewModel()
            {
                DayValue = invoiceDto.PaymentTerm.DayValue,
                Description = invoiceDto.PaymentTerm.Description.Value
            };

            return new InvoiceViewModel()
            {
                Id                = invoiceDto.Id,
                Client            = client,
                Code              = invoiceDto.Code,
                Rnc               = invoiceDto.Rnc,
                Ncf               = invoiceDto.Ncf,
                Created           = invoiceDto.Created,
                DueDate           = invoiceDto.DueDate,
                Footer            = invoiceDto.Footer,
                Notes             = invoiceDto.Notes,
                PaymentTerm       = paymentTerm,
                TermAndConditions = invoiceDto.TermAndConditions,
                Discount          = invoiceDto.Discount,
                SubTotal          = invoiceDto.SubTotal,
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


