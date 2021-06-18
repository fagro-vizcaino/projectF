using ProjectF.Data.Entities.Banks;
using static ProjectF.Application.Banks.BankAccountsMapper;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using LanguageExt;
using ProjectF.Data.Repositories;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Banks
{
    public class BankAccountTypeCrudHandler
    {
        readonly BankAccountTypeRepository _bankAccountTypeRepository;

        public BankAccountTypeCrudHandler(BankAccountTypeRepository bankAccountTypeRepository)
            =>_bankAccountTypeRepository = bankAccountTypeRepository;
        
        public Either<Error, BankAccountType> Create(BankAccountTypeDto bankAccountDto)
            => Validate(bankAccountDto)
            .Bind(SetStatus)
            .Bind(c => Add(FromDto(c)))
            .Bind(Save)
            .ToEither()
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        public Either<Error, BankAccountType> Update(long id, BankAccountTypeDto bankAccountDto)
            => ValidateIsCorrectUpdate(id, bankAccountDto)
            .Bind(Validate)
            .Bind(c => Find(c.Id))
            .Bind(c => UpdateEntity(bankAccountDto, c))
            .Bind(Save)
            .ToEither()
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        public Validation<Error, BankAccountTypeDto> Validate(BankAccountTypeDto bankAccountDto)
            => (ValidateName(bankAccountDto),
                BankAccountTypeNameMustNotExist(bankAccountDto))
            .Map(a => bankAccountDto);

        public Either<Error, BankAccountType> Delete(long id)
          => Find(id)
            .Bind(Delete)
            .Bind(Save)
            .ToEither()
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        public IEnumerable<BankAccountTypeDto> GetAll()
          => _bankAccountTypeRepository.GetAll().Map(b => FromEntity(b));

        public Validation<Error, BankAccountType> Find(params object[] valueKeys)
            => _bankAccountTypeRepository.Find(valueKeys)
            .ToValidation(Error.New($"bank account type id {valueKeys} does not exits"));

        Validation<Error, bool> BankAccountTypeNameMustNotExist(BankAccountTypeDto accountTypeDto)
            => _bankAccountTypeRepository.MustBeUnique(a => a.Name == new Name(accountTypeDto.Name))
            .ToValidation(Error.New($"bank account type name:{accountTypeDto.Name} cannot be duplicate"));

        Validation<Error, BankAccountTypeDto> ValidateIsCorrectUpdate(long id, BankAccountTypeDto bankAccountDto)
            => id == bankAccountDto.Id
            ? bankAccountDto
            : Fail<Error, BankAccountTypeDto>(Error.New("invalid bankaccount type update id"));

        Validation<Error, Name> ValidateName(BankAccountTypeDto bankAccountDto)
            => Name.Of(bankAccountDto.Name);

        Validation<Error, BankAccountTypeDto> SetStatus(BankAccountTypeDto dto)
          => dto with { Status = Data.Entities.Common.EntityStatus.Active };

        Validation<Error, BankAccountType> UpdateEntity(BankAccountTypeDto dto, BankAccountType bankAccountType)
        {
            BankAccountType editAccount = FromDto(dto);

            bankAccountType.EditBankAccountType(editAccount.Name
                , editAccount.Description
                , editAccount.Status);

            return bankAccountType;
        }

        Validation<Error, BankAccountType> Add(BankAccountType bank)
        {
            try
            {
                _bankAccountTypeRepository.Add(bank);
                return bank;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Validation<Error, BankAccountType> Save(BankAccountType bank)
        {
            try
            {
                _bankAccountTypeRepository.Save();
                return bank;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Validation<Error, BankAccountType> Delete(BankAccountType bankAccountType)
        {
            try
            {
                bankAccountType.EditBankAccountType(bankAccountType.Name
                    , bankAccountType.Description
                    , Data.Entities.Common.EntityStatus.Deleted);
                return bankAccountType;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
