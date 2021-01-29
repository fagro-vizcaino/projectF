using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Invoices;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using static ProjectF.Data.Entities.Invoices.InvoiceMapper;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using ProjectF.Application.NumberSequence;

namespace ProjectF.Application.Invoice
{
    public class InvoiceCrudHandler
    {
        readonly InvoiceRepository _invoiceRepository;
        readonly ClientRepository _clientRepository;
        readonly PaymentTermRepository _paymentTermRepository;
        readonly DocumentNumberSequenceHandler _numberSequenceHandler;

        public InvoiceCrudHandler(InvoiceRepository invoiceRepository,
            ClientRepository clientRepository,
            PaymentTermRepository paymentTermRepository,
            DocumentNumberSequenceHandler numberSequenceHandler)
            => (_invoiceRepository,
            _clientRepository,
            _paymentTermRepository,
            _numberSequenceHandler)
            = (invoiceRepository,
            clientRepository,
            paymentTermRepository,
            numberSequenceHandler);


        public Either<Error, InvoiceHeader> Create(InvoiceHeaderDto invoiceDto)
            => ValidateCode(invoiceDto)
            .Bind(ValidateRnc)
            .Bind(ValidateClient)
            .Bind(SetClient)
            .Bind(ValidatePaymentTerm)
            .Bind(SetPaymentTerm)
            .Bind(ValidateNotes)
            .Bind(ValidateTermAndConditions)
            .Bind(ValidateInvoiceDetail)
            .Bind(c => StartTransaction(c))
            .Bind(c => GenerateNumberSequence(FromEntity(c)))
            .Bind(Add)
            .Bind(Save)
            .Bind(UpdateNumberSequence)
            .Bind(CommitTransaction);

        public Either<Error, InvoiceHeader> Update(long id, InvoiceHeaderDto invoiceDto)
          => ValidateIsCorrectUpdate(id, invoiceDto)
           .Bind(ValidateCode)
          .Bind(ValidateRnc)
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
                .Map(i => FromEntity(i));

        public Task<Either<Error, InvoiceHeader>> FindAsync(params object[] valueKeys)
            => _invoiceRepository.FindByAsync(valueKeys);

        public Either<Error, InvoiceHeader> Find(params object[] valueKeys)
            => _invoiceRepository.FindBy(valueKeys).Match(Some: i => i,
                None: Left<Error, InvoiceHeader>(Error.New("No records found")));

        Either<Error, InvoiceHeaderDto> ValidateCode(InvoiceHeaderDto invoiceDto)
           => Code.Of(invoiceDto.Code)
               .Match<Either<Error, InvoiceHeaderDto>>(
                   Left: err => Error.New(err.Message),
                   Right: c => invoiceDto);

        Either<Error, InvoiceHeaderDto> ValidateRnc(InvoiceHeaderDto invoiceDto)
          => invoiceDto.Rnc.Length <= 11
            ? invoiceDto
            : Left<Error, InvoiceHeaderDto>(Error.New("Rnc is greater than 11"));

        Either<Error, InvoiceHeaderDto> ValidateClient(InvoiceHeaderDto invoiceDto)
         => invoiceDto.Client != null
            ? invoiceDto
            : Left<Error, InvoiceHeaderDto>(Error.New("Client shouldn't be null"));

        Either<Error, InvoiceHeaderDto> SetClient(InvoiceHeaderDto invoiceDto)
         => _clientRepository.Find(invoiceDto.ClientId)
                .Match(Some: c => invoiceDto with { Client = c },
                     None: () => Left<Error, InvoiceHeaderDto>(Error.New("couldn't find client")));

        Either<Error, InvoiceHeaderDto> ValidatePaymentTerm(InvoiceHeaderDto invoiceDto)
        => invoiceDto.PaymentTerm != null
           ? invoiceDto
           : Left<Error, InvoiceHeaderDto>(Error.New("paymentTerm shouldn't be null"));

        Either<Error, InvoiceHeaderDto> SetPaymentTerm(InvoiceHeaderDto invoiceDto)
            => _paymentTermRepository.Find(invoiceDto.PaymentTermId)
                .Match(Some: p => invoiceDto with { PaymentTerm = p },
                   None: () => Left<Error, InvoiceHeaderDto>(Error.New("couldn't find payment term")));

        Either<Error, InvoiceHeaderDto> ValidateNotes(InvoiceHeaderDto invoiceDto)
          => GeneralText.Of(invoiceDto.Notes)
            .Match<Either<Error, InvoiceHeaderDto>>(Succ: c => invoiceDto, 
              Fail: err => Error.New(err.Message));

        Either<Error, InvoiceHeaderDto> ValidateTermAndConditions(InvoiceHeaderDto invoiceDto)
           => GeneralText.Of(invoiceDto.Notes)
               .Match<Either<Error, InvoiceHeaderDto>>(Fail: err => Error.New(err.Message),
                   Succ: c => invoiceDto);

        Either<Error, InvoiceHeaderDto> ValidateInvoiceDetail(InvoiceHeaderDto invoiceDto)
            => invoiceDto.InvoiceDetails.Count > 0
                ? invoiceDto
                : Left<Error, InvoiceHeaderDto>(Error.New("Invoice Detail shouldn't be empty"));

        Either<Error, InvoiceHeaderDto> ValidateIsCorrectUpdate(long id, InvoiceHeaderDto invoiceDto)
        {
            if (id == invoiceDto.Id) return invoiceDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, InvoiceHeader> StartTransaction(InvoiceHeaderDto invoice)
        {
            _invoiceRepository.BeginTransaction();
            return FromDto(invoice);
        }

        Either<Error, InvoiceHeader> UpdateEntity(InvoiceHeaderDto invoiceDto, InvoiceHeader invoice)
        {
            var code              = new Code(invoiceDto.Code);
            var rnc               = invoiceDto.Rnc;
            var client            = invoiceDto.Client;
            var dueDate           = invoiceDto.DueDate;
            var paymentTerm       = invoiceDto.PaymentTerm;
            var notes             = new GeneralText(invoiceDto.Notes);
            var termAndConditions = new GeneralText(invoiceDto.TermAndConditions);
            var footer            = new GeneralText(invoiceDto.Footer);
            var invoiceDetails    = invoiceDto.InvoiceDetails;

            invoice.EditInvoiceHeader(code,
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
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, InvoiceHeader> CommitTransaction(InvoiceHeader invoiceHeader)
        {
            try
            {
                _invoiceRepository.EndTransaction();
                return Right(invoiceHeader);
            }
            catch (Exception e)
            {
                return Left(Error.New(e.Message));
            }

        }

        Either<Error, InvoiceHeader> UpdateNumberSequence(InvoiceHeader invoice)
            => _numberSequenceHandler.UpdateSequence(invoice.NumberSequenceId, 1)
                .Match<Either<Error, InvoiceHeader>>(Left: err => Error.New(err.Message),
                    Right: c => invoice);

        Either<Error, InvoiceHeader> GenerateNumberSequence(InvoiceHeaderDto invoice)
            => _numberSequenceHandler.GenerateSequence(invoice.NumberSequenceId)
               .Map(c => FromDto(invoice with { Ncf = c }));
    }
}
