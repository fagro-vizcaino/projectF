using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common;
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
        public EntityStatus Status { get; set; }
        
        public BankAccountDto ToDto()
            => new BankAccountDto(AccountName, AccountNumber, Description, InitialBalance, BankAccountTypeId,
                
                new BankAccountTypeDto(BankAccountType.Name, 
                    BankAccountType.Description)
                {
                    Id =  BankAccountType.Id,
                    Created = BankAccountType.Created,
                    Modified = BankAccountType.Modified,
                    Status = BankAccountType.Status
                });

        public static BankAccountViewModel FromDto(BankAccountDto accountDto)
            => new()
            {
                Id                = accountDto.Id,
                AccountName       = accountDto.AccountName,
                AccountNumber     = accountDto.AccountNumber,
                BankAccountTypeId = accountDto.BankAccountTypeId,
                BankAccountType   = new BankAccountTypeViewModel()
                {
                    Id          = accountDto.BankAccountType.Id,
                    Description = accountDto.BankAccountType.Description,
                    Name        = accountDto.BankAccountType.Name
                },
                Description     = accountDto.Description,
                InitialBalance  = accountDto.InitialBalance,
                Created         = accountDto.Created,
                Modified        = accountDto.Modified
            };
    }
}
