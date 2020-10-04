﻿    using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.PaymentTerms
{
    public class PaymentTermCrudHandler
    {
        readonly PaymentTermRepository _paymentTermRepository;

        public PaymentTermCrudHandler(PaymentTermRepository paymentTermRepository)
            => _paymentTermRepository = paymentTermRepository;

        public Either<Error, PaymentTerm> Create(PaymentTermDto paymentTermDto)
            => ValidateName(paymentTermDto)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);

        public Either<Error, PaymentTerm> Update(long id, PaymentTermDto paymentTermDto)
            => ValidateIsCorrectUpdate(id, paymentTermDto)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(paymentTermDto, c))
            .Bind(Save);

        public IEnumerable<PaymentTermDto> GetAll()
            => _paymentTermRepository.GetAll().Map(pt => (PaymentTermDto) pt);

        public Either<Error, PaymentTerm> Find(params object[] valueKeys)
            => _paymentTermRepository
            .Find(valueKeys).Match(Some: p => p, 
                None: Left<Error, PaymentTerm>(Error.New("Couldn't find payment term")));

        public Either<Error, PaymentTerm> Delete(long id)
           => Find(id)
             .Bind(Delete)
             .Bind(Save);

        //Missing Pagination
        Either<Error, PaymentTermDto> ValidateIsCorrectUpdate(long id, PaymentTermDto paymentTermDto)
        {
            if (id == paymentTermDto.Id) return paymentTermDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, PaymentTermDto> ValidateName(PaymentTermDto paymentTermDto)
            => Name.Of(paymentTermDto.Description)
                .Match(Succ: c => paymentTermDto,
                 Fail: err => Left<Error, PaymentTermDto>(Error.New(string.Join(";", err))));

        Either<Error, PaymentTerm> CreateEntity(PaymentTermDto paymentTermDto)
            => Right<Error, PaymentTerm>(paymentTermDto);

        Either<Error, PaymentTerm> UpdateEntity(PaymentTermDto paymentTermDto, PaymentTerm paymentTerm)
        {
            PaymentTerm editedPaymentTerm = paymentTermDto;
            paymentTerm.EditPaymentDeadlines(editedPaymentTerm.Description, editedPaymentTerm.DayValue);
            return paymentTerm;
        }

        Either<Error, PaymentTerm> Add(PaymentTerm paymentTerm)
        {
            try
            {
                _paymentTermRepository.Add(paymentTerm);
                return paymentTerm;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, PaymentTerm> Save(PaymentTerm paymentTerm)
        {
            try
            {
                _paymentTermRepository.Save();
                return paymentTerm;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, PaymentTerm> Delete(PaymentTerm paymentTerm)
        {
            try
            {
                _paymentTermRepository.Delete(paymentTerm);
                return paymentTerm;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
