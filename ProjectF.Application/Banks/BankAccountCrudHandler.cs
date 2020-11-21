using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using static ProjectF.Application.Validator.Validators;
using System;
using System.Collections.Generic;
using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common.ValueObjects;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;
using System.Linq;

namespace ProjectF.Application.Banks
{
    public class BankAccountCrudHandler
    {
        readonly BankAccountRepository _bankRepository;
        readonly BankAccountTypeRepository _bankAccountTypeRepository;
        public BankAccountCrudHandler(BankAccountRepository bankRepository,
            BankAccountTypeRepository bankAccountTypeRepository)
            => (_bankRepository,
            _bankAccountTypeRepository)
            = (bankRepository, bankAccountTypeRepository);

        public Either<Error, BankAccount> Create(BankAccountDto bankAccountDto)
            => Validate(bankAccountDto)
            .Bind(CreateEntity)
            .Bind(Add)
            .Bind(Save)
            .ToEither()
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        public Either<Error, BankAccount> Update(long id, BankAccountDto bankAccountDto)
            => UpdateOperation(id, bankAccountDto)
            .Bind(Save)
            .ToEither()
            .MapLeft(errors => Error.New(string.Join("; ", errors)));
            
        public Validation<Error, BankAccount> UpdateOperation(long id, BankAccountDto bankAccountDto) 
            => (Find(id), 
                Validate(bankAccountDto), 
                ValidateIsCorrectUpdate(id, bankAccountDto))
            .Apply((editObject, bankAccountObj, c) => UpdateEntity(bankAccountObj, editObject))
            .Map(c => c);

        public Task<Either<Error, (List<BankAccountDto> list, MetaData meta)>> GetBankAccountList(BankListParameters listParameters)
            => _bankRepository.GetBankAccountListAsync(listParameters, true)
            .MapT(c => (c.Select(i => (BankAccountDto)i).ToList(), c.MetaData));
        
        public Validation<Error, BankAccount> Find(long id)
            => _bankRepository.Get(id)
            .Match(Some: c => Success<Error, BankAccount>(c),
                None: Error.New($"bank account id {id} does not exits"));

        public Either<Error, BankAccount> Delete(long id)
          => Find(id)            
            .Bind(Delete)
            .Bind(Save)
            .ToEither()
            .MapLeft(errors => Error.New(string.Join("; ", errors)));

        Validation<Error, BankAccountDto> Validate(BankAccountDto bankAccountDto)
            => (ValidateName(bankAccountDto),
                ValidateAccountNumber(bankAccountDto),
                InitialBalanceIsRequired(bankAccountDto),
                ValidateAccountType(bankAccountDto),
                AccountTypeMustExist(bankAccountDto),
                BankAccountNameMustNotExist(bankAccountDto))
            .Apply((z, y, x, w, g, f) => bankAccountDto.With(bankAccountType: g))
            .Map(b => b);

        Validation<Error, bool> ValidateAccountNumber(BankAccountDto bankAccountDto)
            => _bankRepository.MustBeUnique(bankAccountDto.AccountNumber.Trim())
            .ToValidation(Error.New($"Account number:{bankAccountDto.AccountNumber} cannot be duplicate"));

        Validation<Error, BankAccountType> AccountTypeMustExist(BankAccountDto bankAccountDto)
          => _bankAccountTypeRepository.Get(bankAccountDto.BankAccountTypeId)
          .ToValidation(Error.New($"bank account type {bankAccountDto.BankAccountTypeId} does not exits"));

        Validation<Error, BankAccountDto> ValidateAccountType(BankAccountDto bankAccountDto)
         => bankAccountDto.BankAccountTypeId > 0
                ? Success<Error, BankAccountDto>(bankAccountDto)
                : Fail<Error, BankAccountDto>(Error.New("bank account type is require"));

        Validation<Error, bool> BankAccountNameMustNotExist(BankAccountDto bankAccountDto)
            => _bankRepository.MustBeUnique(bankAccountDto.AccountName)
            .ToValidation(Error.New($"bank account name:{bankAccountDto.AccountName} cannot be duplicate"));

        Validation<Error, decimal> InitialBalanceIsRequired(BankAccountDto bankAccountDto)
            => AtLeast(0m)(bankAccountDto.InitialBalance).Bind(AtMost(99999999.99m));

        Validation<Error, BankAccountDto> ValidateIsCorrectUpdate(long id, BankAccountDto bankAccountDto)
            => id == bankAccountDto.Id 
            ? bankAccountDto 
            : Fail<Error, BankAccountDto>(Error.New("invalid bankaccount update id"));

        Validation<Error, Name> ValidateName(BankAccountDto bankAccountDto)
            => Name.Of(bankAccountDto.AccountName);

        Validation<Error, BankAccount> CreateEntity(BankAccountDto bankAccountDto)
         => Success<Error, BankAccount>(bankAccountDto);

        BankAccount UpdateEntity(BankAccountDto bankAccountDto, BankAccount bankAccount)
        {
            BankAccount editAccount = bankAccountDto;
            
            bankAccount.EditBank(editAccount.AccountName,
                editAccount.AccountNumber,
                editAccount.Description,
                editAccount.InitialBalance,
                editAccount.BankAccountType,
                editAccount.Created);
            return bankAccount;
        }

        Validation<Error, BankAccount> Add(BankAccount bank)
        {
            try
            {
                _bankRepository.Add(bank);
                return bank;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Validation<Error, BankAccount> Save(BankAccount bank)
        {
            try
            {
                _bankRepository.Save();
                return bank;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }

        Validation<Error, BankAccount> Delete(BankAccount bankAccount)
        {
            try
            {
                _bankRepository.Delete(bankAccount);
                return bankAccount;
            }
            catch (Exception ex)
            {
                return Error.New($"{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
