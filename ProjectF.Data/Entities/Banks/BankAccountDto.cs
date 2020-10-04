using ProjectF.Data.Entities.Common.ValueObjects;
using System;

namespace ProjectF.Data.Entities.Banks
{
    public class BankAccountDto
    {
        public long Id { get; }
        public string AccountName { get; }
        public string AccountNumber { get; }
        public string Description { get; }
        public decimal InitialBalance { get; }
        public long BankAccountTypeId { get;}
        public BankAccountType BankAccountType { get; }
        public DateTime Created { get; }
        public DateTime? Modified { get; }

        public BankAccountDto(long id, 
            string accountName,
            string accountNumber,
            string description,
            decimal initialBalance,
            long bankAccountTypeId,
            BankAccountType bankAccountType,
            DateTime created,
            DateTime? modified)
        {
            Id                = id;
            AccountName       = accountName.Trim();
            AccountNumber     = accountNumber;
            Description       = description;
            InitialBalance    = initialBalance;
            BankAccountTypeId = bankAccountTypeId;
            BankAccountType   = bankAccountType;
            Created           = created;
            Modified          = modified;
        }


        public BankAccountDto With(long? id = null,
            string? accountName = null,
            string? accountNumber = null,
            string? description = null,
            decimal? initialBalance = null,
            DateTime? created = null,
            DateTime? modified = null,
            long? bankAccountTypeId = null,
            BankAccountType? bankAccountType = null
            ) => new BankAccountDto(id ?? this.Id,
                accountName ?? this.AccountName,
                accountNumber ?? this.AccountNumber,
                description ?? this.Description,
                initialBalance ?? this.InitialBalance,
                bankAccountTypeId ?? this.BankAccountTypeId,
                bankAccountType ?? this.BankAccountType,
                created ?? this.Created,
                modified ?? this.Modified);


       public void Deconstruct(out long id, 
            out string accountName,
            out string accountNumber,
            out string description,
            out decimal initialBalance,
            out long bankAccountTypeId,
            out BankAccountType bankAccountType,
            out DateTime created,
            out DateTime? modified)
        {
            id                = Id;
            accountName       = AccountName;
            accountNumber     = AccountNumber;
            description       = Description;
            initialBalance    = InitialBalance;
            created           = Created;
            modified          = Modified;
            bankAccountTypeId = BankAccountTypeId;
            bankAccountType   = BankAccountType;
        }


        public static implicit operator BankAccount(BankAccountDto dto)
            =>  new BankAccount(
                    new Name(dto.AccountName),
                    dto.AccountNumber,
                    new GeneralText(dto.Description),
                    dto.InitialBalance,
                    dto.BankAccountType,
                    dto.Created);
    }
}
