using ProjectF.Data.Entities.Banks;
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
            .Bind(CreateEntity)
            .Bind(Add)
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

        public IEnumerable<BankAccountTypeDto> GetAll()
          => _bankAccountTypeRepository.GetAll().Map(b => (BankAccountTypeDto)b);

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

        Validation<Error, BankAccountType> CreateEntity(BankAccountTypeDto bankAccountDto)
         => Success<Error, BankAccountType>(bankAccountDto);

        Validation<Error, BankAccountType> UpdateEntity(BankAccountTypeDto accountTypeDto, BankAccountType bankAccountType)
        {
            BankAccountType editAccount = accountTypeDto;

            bankAccountType.EditBankAccountType(editAccount.Name,                
                editAccount.Description);

            return bankAccountType;
        }

        Validation<Error, BankAccountType> Add(BankAccountType bank)
        {
            try
            {
                _bankAccountTypeRepository.Add(bank);
                return bank;
            }
            catch (System.Exception ex)
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
    }
}
