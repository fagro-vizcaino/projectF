using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using ProjectF.Data.Repositories;
using ProjectF.Data.Entities;
using System;
using System.Collections.Generic;
using ProjectF.Data.Entities.Sequences;

namespace ProjectF.Application.DocumentNumberSequence
{
    public class DocumentNumberSequenceHandler
    {
        readonly DocumentNumberSequenceRepository _documentNumberSequenceRepository;

        //public DocumentNumberSequenceHandler(DocumentNumberSequenceRepository documentNumberSequenceRepository)
        //    => _documentNumberSequenceRepository = documentNumberSequenceRepository;

        //public Either<Error, NumberSequenceDto> Create(NumberSequenceDto numberSequenceDto)
        //    => ValidateName(paymentTermDto)
        //    .Bind(CreateEntity)
        //    .Bind(Add)
        //    .Bind(Save);

        //public Either<Error, NumberSequenceDto> Update(long id, NumberSequenceDto numberSequenceDto)
        //    => ValidateIsCorrectUpdate(id, paymentTermDto)
        //    .Bind(ValidateName)
        //    .Bind(c => Find(c.Id))
        //    .Bind(c => UpdateEntity(paymentTermDto, c))
        //    .Bind(Save);

        //public IEnumerable<NumberSequenceDto> GetAll()
        //    => _paymentTermRepository.GetAll().Map(pt => (PaymentTermDto)pt);

        //public Either<Error, NumberSequenceDto> Find(params object[] valueKeys)
        //    => _paymentTermRepository
        //    .Find(valueKeys).Match(Some: p => p,
        //        None: Left<Error, PaymentTerm>(Error.New("Couldn't find payment term")));

        //public Either<Error, NumberSequenceDto> Delete(long id)
        //   => Find(id)
        //     .Bind(Delete)
        //     .Bind(Save);

        ////Missing Pagination
        //Either<Error, NumberSequenceDto> ValidateIsCorrectUpdate(long id, NumberSequenceDto numberSequenceDto)
        //{
        //    if (id == paymentTermDto.Id) return paymentTermDto;
        //    return Error.New("Invalid update entity id");
        //}

        //Either<Error, NumberSequenceDto> ValidateName(NumberSequenceDto numberSequenceDto)
        //    => Name.Of(paymentTermDto.Description)
        //        .Match(Succ: c => paymentTermDto,
        //         Fail: err => Left<Error, PaymentTermDto>(Error.New(string.Join(";", err))));

        //Either<Error, PaymentTerm> CreateEntity(PaymentTermDto paymentTermDto)
        //    => Right<Error, PaymentTerm>(paymentTermDto);

        //Either<Error, PaymentTerm> UpdateEntity(PaymentTermDto paymentTermDto, PaymentTerm paymentTerm)
        //{
        //    PaymentTerm editedPaymentTerm = paymentTermDto;
        //    paymentTerm.EditPaymentDeadlines(editedPaymentTerm.Description, editedPaymentTerm.DayValue);
        //    return paymentTerm;
        //}

        //Either<Error, PaymentTerm> Add(PaymentTerm paymentTerm)
        //{
        //    try
        //    {
        //        _paymentTermRepository.Add(paymentTerm);
        //        return paymentTerm;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Error.New($"{ex.Message}\n{ex.StackTrace}");
        //    }
        //}

        //Either<Error, PaymentTerm> Save(PaymentTerm paymentTerm)
        //{
        //    try
        //    {
        //        _paymentTermRepository.Save();
        //        return paymentTerm;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return Error.New($"{ex.Message}\n{ex.StackTrace}");
        //    }
        //}

        //Either<Error, PaymentTerm> Delete(PaymentTerm paymentTerm)
        //{
        //    try
        //    {
        //        _paymentTermRepository.Delete(paymentTerm);
        //        return paymentTerm;
        //    }
        //    catch (Exception ex)
        //    {
        //        return Error.New($"{ex.Message}\n{ex.StackTrace}");
        //    }
        //}
    }
}
