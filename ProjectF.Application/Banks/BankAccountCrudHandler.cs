using ProjectF.Data.Repositories;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using static ProjectF.Application.Validator.Validators;
using System;
using System.Collections.Generic;
using ProjectF.Data.Entities.Banks;
using static ProjectF.Application.Banks.BankAccountsMapper;
using ProjectF.Data.Entities.Common.ValueObjects;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;
using System.Linq;
using ProjectF.Application.Common;

namespace ProjectF.Application.Banks
{
    public class BankAccountCrudHandler : BaseCrudHandler<BankAccountDto, BankAccount, BankAccountRepository>
    {
        private readonly BankAccountRepository _bankRepository;
        readonly BankAccountTypeRepository _bankAccountTypeRepository;
        public BankAccountCrudHandler(BankAccountRepository bankRepository,
            BankAccountTypeRepository bankAccountTypeRepository) : base(bankRepository)
            => (_bankRepository, _bankAccountTypeRepository, _fromDto, _fromEntity, _updateEntity)
            = (bankRepository, bankAccountTypeRepository, FromDto, FromEntity, UpdateEntity);
        
        public Task<Option<BankAccountMainListDto>> GetBankAccountList(BankListParameters listParameters)
            => _bankRepository.GetBankAccountListAsync(listParameters, true)
            .MapT(c => new BankAccountMainListDto(c.Select(FromEntity).ToList(), c.MetaData));

        public Validation<Error, BankAccount> Find(long id)
            => _bankRepository.Get(id)
            .Match(Some: c => Success<Error, BankAccount>(c),
                None: Error.New($"bank account id {id} does not exits"));

        Either<Error, BankAccount> UpdateEntity(BankAccountDto bankAccountDto, BankAccount bankAccount)
        {
            BankAccount editAccount = FromDto(bankAccountDto);

            bankAccount.EditBank(editAccount.AccountName,
                editAccount.AccountNumber,
                editAccount.Description,
                editAccount.InitialBalance,
                editAccount.BankAccountType,
                editAccount.Status);
            return bankAccount;
        }
    }
}
