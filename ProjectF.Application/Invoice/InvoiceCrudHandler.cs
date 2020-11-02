using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace ProjectF.Application.Invoice
{
    public class InvoiceCrudHandler
    {
        readonly InvoiceRepository _invoiceRepository;
        readonly ProductRepository _productRepository;
        readonly CategoryRepository _categoryRepository;
        readonly WerehouseRepository _werehouseRepository;
        readonly CountryRepository _countryRepository;
        readonly ClientRepository _clientRepository;
        readonly PaymentTermRepository _paymentTermRepository;

        public InvoiceCrudHandler(InvoiceRepository invoiceRepository,
            ProductRepository productRepository,
            CategoryRepository categoryRepository,
            WerehouseRepository werehouseRepository,
            ClientRepository clientRepository,
            PaymentTermRepository paymentTermRepository,
            CountryRepository countryRepository)
        {
            (_invoiceRepository,
                       _productRepository,
                       _categoryRepository,
                       _werehouseRepository,
                       _countryRepository,
                       _clientRepository,
                       _paymentTermRepository)
                       = (invoiceRepository,
                       productRepository,
                       categoryRepository,
                       werehouseRepository,
                       countryRepository,
                       clientRepository,
                       paymentTermRepository);
        }

        public Either<Error, InvoiceHeader> Create(InvoiceHeaderDto invoiceDto)
            => ValidateCode(invoiceDto)
            .Bind(ValidateRnc)
            .Bind(ValidateNcf)
            .Bind(ValidateClient)
            .Bind(SetClient)
            .Bind(ValidatePaymentTerm)
            .Bind(SetPaymentTerm)
            .Bind(ValidateNotes)
            .Bind(ValidateTermAndConditions)
            .Bind(ValidateInvoiceDetail)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);

        public Either<Error, InvoiceHeader> Update(long id, InvoiceHeaderDto invoiceDto)
          => ValidateIsCorrectUpdate(id, invoiceDto)
           .Bind(ValidateCode)
          .Bind(ValidateRnc)
          .Bind(ValidateNcf)
          .Bind(ValidateClient)
          .Bind(ValidatePaymentTerm)
          .Bind(ValidateNotes)
          .Bind(ValidateTermAndConditions)
          .Bind(ValidateInvoiceDetail)
          .Bind(i => Find(i.Id))
          .Bind(i => UpdateEntity(invoiceDto, i))
          .Bind(Save);

        public IEnumerable<InvoiceHeaderDto> GetAll()
            => _invoiceRepository.GetAll()
                .Map(i => new InvoiceHeaderDto(i.Id,
                    i.Code.Value,
                    i.Ncf.Value.ToString(),
                    i.Rnc,
                    i.Client?.Id ?? 0,
                    i.Client,
                    i.Created,
                    i.DueDate,
                    i.PaymentTerm?.Id ?? 0,
                    i.PaymentTerm,
                    i.Notes.Value,
                    i.TermAndConditions.Value,
                    i.Footer.Value,
                    i.Discount,
                    i.SubTotal,
                    i.TaxTotal,
                    i.Total,
                    i.InvoiceDetails
                        .ToList()
                        .Map(d => new InvoiceDetailDto(d.Id, 
                            d.ProductCode.Value, 
                            d.Description.Value, 
                            d.Qty, 
                            d.Amount, 
                            d.TaxPercent)
                    )));

        public Task<Either<Error, InvoiceHeader>> FindAsync(params object[] valueKeys)
            => _invoiceRepository.FindByAsync(valueKeys);

        public Either<Error, InvoiceHeader> Find(params object[] valueKeys)
            => _invoiceRepository.FindBy(valueKeys).Match(Some: i => i,
                None: Left<Error, InvoiceHeader>(Error.New("No records found")));

        public InvoiceHeaderDto EntityToDto(InvoiceHeader invoice)
             => new InvoiceHeaderDto(invoice.Id,
                 invoice.Code.Value,
                 invoice.Ncf.Value.ToString(),
                 invoice.Rnc,
                 invoice.Client?.Id ?? 0,
                 invoice.Client,
                 invoice.Created,
                 invoice.DueDate,
                 invoice.PaymentTerm?.Id ?? 0,
                 invoice.PaymentTerm,
                 invoice.Notes?.Value ?? string.Empty,
                 invoice.TermAndConditions?.Value ?? string.Empty,
                 invoice.Footer?.Value ?? string.Empty,
                 invoice.Discount,
                 invoice.SubTotal,
                 invoice.TaxTotal,
                 invoice.Total,
                 invoice.InvoiceDetails
                    .ToList()
                    .Map(d => new InvoiceDetailDto(d.Id, d.ProductCode.Value, d.Description.Value, d.Qty, d.Amount, d.TaxPercent)));

        Either<Error, InvoiceHeaderDto> ValidateCode(InvoiceHeaderDto invoiceDto)
           => Code.Of(invoiceDto.Code)
               .Match<Either<Error, InvoiceHeaderDto>>(
                   Left: err => Error.New(err.Message),
                   Right: c => invoiceDto);

        Either<Error, InvoiceHeaderDto> ValidateRnc(InvoiceHeaderDto invoiceDto)
          => invoiceDto.Rnc.Length <= 11
            ? invoiceDto
            : Left<Error, InvoiceHeaderDto>(Error.New("Rnc is greater than 11"));

        Either<Error, InvoiceHeaderDto> ValidateNcf(InvoiceHeaderDto invoiceDto)
        {
            var _ncfNumber = string.Join("", invoiceDto.Ncf[3..]);
            long.TryParse(_ncfNumber, out long ncfNumber);
            return NumberSequence.Of(ncfNumber)
               .Match<Either<Error, InvoiceHeaderDto>>(
                    Right: c => invoiceDto,
                    Left: err => Error.New(err.Message));
        }

        Either<Error, InvoiceHeaderDto> ValidateClient(InvoiceHeaderDto invoiceDto)
         => invoiceDto.Client != null
            ? invoiceDto
            : Left<Error, InvoiceHeaderDto>(Error.New("Client shouldn't be null"));

        Either<Error, InvoiceHeaderDto> SetClient(InvoiceHeaderDto invoiceDto)
         => _clientRepository.Find(invoiceDto.ClientId)
                .Match(Some: c => invoiceDto.With(client: c),
                     None: () => Left<Error, InvoiceHeaderDto>(Error.New("couldn't find client")));
        
        Either<Error, InvoiceHeaderDto> ValidatePaymentTerm(InvoiceHeaderDto invoiceDto)
        => invoiceDto.PaymentTerm != null
           ? invoiceDto
           : Left<Error, InvoiceHeaderDto>(Error.New("paymentTerm shouldn't be null"));

        Either<Error, InvoiceHeaderDto> SetPaymentTerm(InvoiceHeaderDto invoiceDto)
            => _paymentTermRepository.Find(invoiceDto.PaymentTermId)
                .Match(Some: p => invoiceDto.With(paymentTerm: p),
                   None: () => Left<Error, InvoiceHeaderDto>(Error.New("couldn't find payment term")));
        
        Either<Error, InvoiceHeaderDto> ValidateNotes(InvoiceHeaderDto invoiceDto)
          => GeneralText.Of(invoiceDto.Notes)
              .Match<Either<Error, InvoiceHeaderDto>>(
                  Left: err => Error.New(err.Message),
                  Right: c => invoiceDto);

        Either<Error, InvoiceHeaderDto> ValidateTermAndConditions(InvoiceHeaderDto invoiceDto)
           => GeneralText.Of(invoiceDto.Notes)
               .Match<Either<Error, InvoiceHeaderDto>>(
                   Left: err => Error.New(err.Message),
                   Right: c => invoiceDto);

        Either<Error, InvoiceHeaderDto> ValidateInvoiceDetail(InvoiceHeaderDto invoiceDto)
            => invoiceDto.InvoiceDetails.Count > 0
                ? invoiceDto
                : Left<Error, InvoiceHeaderDto>(Error.New("Invoice Detail shouldn't be empty"));

        Either<Error, InvoiceHeaderDto> ValidateIsCorrectUpdate(long id, InvoiceHeaderDto invoiceDto)
        {
            if (id == invoiceDto.Id) return invoiceDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, InvoiceHeader> CreateEntity(InvoiceHeaderDto invoice)
        {
            var (_,
                code,
                ncf,
                rnc,
                client,
                clientId,
                created,
                dueDate,
                paymentTermId,
                paymentTerm,
                notes,
                termAndConditions,
                footer,
                discount,
                subTotal,
                taxTotal,
                total,
                _, invoiceDetails) = invoice;

            return new InvoiceHeader(new Code(code),
                new NumberSequence(long.Parse(ncf[3..])),
                ncf[..3],
                rnc,
                client,
                created,
                dueDate,
                paymentTerm,
                new GeneralText(notes),
                new GeneralText(termAndConditions),
                new GeneralText(footer),
                discount,
                subTotal,
                taxTotal,
                total,
                invoiceDetails
                    .ToList());

        }

        Either<Error, InvoiceHeader> UpdateEntity(InvoiceHeaderDto invoiceDto, InvoiceHeader invoice)
        {
            var code              = new Code(invoiceDto.Code);
            var ncf               = new NumberSequence(long.Parse(invoiceDto.Ncf[3..]));
            var ncfType           = invoiceDto.Ncf[..2];
            var rnc               = invoiceDto.Rnc;
            var client            = invoiceDto.Client;
            var dueDate           = invoiceDto.DueDate;
            var paymentTerm       = invoiceDto.PaymentTerm;
            var notes             = new GeneralText(invoiceDto.Notes);
            var termAndConditions = new GeneralText(invoiceDto.TermAndConditions);
            var footer            = new GeneralText(invoiceDto.Footer);
            var invoiceDetails    = invoiceDto.InvoiceDetails;

            invoice.EditInvoiceHeader(code,
                ncf,
                ncfType,
                rnc,
                client,
                dueDate,
                paymentTerm,
                notes,
                termAndConditions,
                footer,
                invoiceDetails);

            return invoice;
        }

        Either<Error, InvoiceHeader> Add(InvoiceHeader invoice)
        {
            try
            {
                _invoiceRepository.Add(invoice);
                return invoice;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, InvoiceHeader> Save(InvoiceHeader invoice)
        {
            try
            {
                _invoiceRepository.Save();
                return invoice;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
