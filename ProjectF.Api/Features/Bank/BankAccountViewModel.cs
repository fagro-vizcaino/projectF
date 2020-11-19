using ProjectF.Data.Entities.Banks;
using System;

namespace ProjectF.Api.Features.Bank
{
    public class BankAccountViewModel
    {
        public long Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public decimal InitialBalance { get; set; }
        public long BankAccountTypeId { get; set; }
        public BankAccountTypeViewModel? BankAccountType { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public BankAccountDto ToDto()
            => new BankAccountDto(Id,
                AccountName,
                AccountNumber,
                Description,
                InitialBalance,
                BankAccountTypeId,
                bankAccountType: null,
                Created,
                Modified);

        public static BankAccountViewModel FromDto(BankAccountDto accountDto)
            => new BankAccountViewModel()
            {
                Id                = accountDto.Id,
                AccountName       = accountDto.AccountName,
                AccountNumber     = accountDto.AccountNumber,
                BankAccountTypeId = accountDto.BankAccountTypeId,
                BankAccountType   = new BankAccountTypeViewModel()
                {
                    Id          = accountDto.BankAccountType.Id,
                    Description = accountDto.BankAccountType.Description.Value,
                    Name        = accountDto.BankAccountType.Name.Value
                },
                Description     = accountDto.Description,
                InitialBalance  = accountDto.InitialBalance,
                Created         = accountDto.Created,
                Modified        = accountDto.Modified
            };
    }
}
