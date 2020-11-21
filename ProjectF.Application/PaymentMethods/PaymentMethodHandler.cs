using System;
using System.Collections.Generic;
using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.PaymentList;
using ProjectF.Data.Entities.PaymentMethods;
using ProjectF.Data.Repositories;
using static ProjectF.Data.Entities.PaymentMethods.PaymentMethodMapper;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Application.PaymentMethods
{
    public class PaymentMethodHandler
    {
        readonly PaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodHandler(PaymentMethodRepository paymentMethodRepository)
            => _paymentMethodRepository = paymentMethodRepository;

        public Either<Error, PaymentMethod> Create(PaymentMethodDto dto)
            => ValidateName(dto)
            .Bind(c => Add(FromDto(c)))
            .Bind(Save);

        public Either<Error, PaymentMethod> Update(long id, PaymentMethodDto dto)
            => ValidateIsCorrectUpdate(id, dto)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(c, dto))
            .Bind(Save);

        public IEnumerable<PaymentMethodDto> GetAll()
            => _paymentMethodRepository.GetAll().Map(pt => FromEntity(pt));

        public Either<Error, PaymentMethod> Find(params object[] valueKeys)
            => _paymentMethodRepository
            .Find(valueKeys).Match(Some: p => p,
                None: Left<Error, PaymentMethod>(Error.New("Couldn't find payment method")));

        public Either<Error, PaymentMethod> Delete(long id)
           => Find(id)
             .Bind(Delete)
             .Bind(Save);

        //Missing Pagination
        Either<Error, PaymentMethodDto> ValidateIsCorrectUpdate(long id, PaymentMethodDto dto)
        {
            if (id == dto.Id) return dto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, PaymentMethodDto> ValidateName(PaymentMethodDto dto)
            => Name.Of(dto.Description)
                .Match(Succ: c => dto,
                 Fail: err => Left<Error, PaymentMethodDto>(Error.New(string.Join(";", err))));

        Either<Error, PaymentMethod> UpdateEntity(PaymentMethod editing, PaymentMethodDto dto)
        {
            editing.EditPaymentMethod(new Name(dto.Description), dto.Status);
            return editing;
        }

        Either<Error, PaymentMethod> Add(PaymentMethod paymentMethod)
        {
            try
            {
                _paymentMethodRepository.Add(paymentMethod);
                return paymentMethod;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, PaymentMethod> Save(PaymentMethod paymentMethod)
        {
            try
            {
                _paymentMethodRepository.Save();
                return paymentMethod;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, PaymentMethod> Delete(PaymentMethod paymentMethod)
        {
            try
            {
                paymentMethod.EditPaymentMethod(paymentMethod.Description, EntityStatus.Deleted);
                return paymentMethod;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
