﻿using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Taxes;
using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Application.Taxes
{
    public class TaxCrudHandler
    {
        readonly TaxRepository _taxRepository;

        public TaxCrudHandler(TaxRepository taxRepository) 
            => _taxRepository = taxRepository;
        

        public Either<Error, Tax> Create(TaxDto taxDto)
            => ValidateName(taxDto)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save);


        public Either<Error, Tax> Update(long id, TaxDto taxDto)
            => ValidateIsCorrectUpdate(id, taxDto)
            .Bind(ValidateName)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(taxDto, c))
            .Bind(Save);

        public Either<Error, Tax> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save);

        public IEnumerable<TaxDto> GetAll()
            => _taxRepository.GetAll()
                .Map(t => new TaxDto(t.Id, t.Name.Value, t.PercentValue));

        public Either<Error, Tax> Find(params object[] valueKeys)
           =>  _taxRepository.Find(valueKeys).Match(Some: p => p, 
                None: Left<Error, Tax>(Error.New("Couldn't find tax")));

        public TaxDto EntityToDto(Tax tax)
           => new TaxDto(tax.Id, tax.Name.Value, tax.PercentValue);

        //Missing Pagination

        Either<Error, TaxDto> ValidateIsCorrectUpdate(long id, TaxDto categoryDto)
        {
            if (id == categoryDto.Id) return categoryDto;
            return Error.New("Invalid update entity id");
        }

        Either<Error, TaxDto> ValidateName(TaxDto taxDto)
            => Name.Of(taxDto.Name).Match(Succ: c => taxDto,
                 Fail: err => Left<Error, TaxDto>(Error.New(string.Join(";", err))));

        Either<Error, Tax> CreateEntity(TaxDto taxDto)
        {
            var name = new Name(taxDto.Name);
            var tax = new Tax(name, taxDto.PercentValue);

            return tax;
        }

        Either<Error, Tax> UpdateEntity(TaxDto taxDto, Tax tax)
        {
            var name = new Name(taxDto.Name);
            tax.EditTax(name, tax.PercentValue);
            return tax;
        }

        Either<Error, Tax> Add(Tax tax)
        {
            try
            {
                _taxRepository.Add(tax);
                return tax;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Tax> Save(Tax tax)
        {
            try
            {
                _taxRepository.Save();
                return tax;
            }
            catch (System.Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Either<Error, Tax> Delete(Tax tax)
        {
            try
            {
                _taxRepository.Delete(tax);
                return tax;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
