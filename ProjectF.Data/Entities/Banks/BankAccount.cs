using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Banks
{
    public class BankAccount : _BaseEntity
    {
        public Name AccountName { get; private set; }
        public string AccountNumber { get; private set; }
        public GeneralText Description { get; private set; }
        public decimal InitialBalance { get; private set; }
        public virtual BankAccountType BankAccountType { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime? Modified { get; private set; }

        protected BankAccount() { }

        public BankAccount(Name accountName,
            string accountNumber,
            GeneralText description,
            decimal initialBalance,
            BankAccountType bankAccountType,
            DateTime created)
        {
            AccountName      = accountName;
            AccountNumber    = accountNumber;
            Description      = description;
            InitialBalance   = initialBalance;
            AccountNumber    = accountNumber;
            BankAccountType  = bankAccountType;
            Created          = created;
        }

        public void EditBank(Name accountName,
            string accountNumber,
            GeneralText description,
            decimal initialBalance,
            BankAccountType bankAccountType,
            DateTime created)
        {
            AccountName      = accountName;
            AccountNumber    = accountNumber;
            Description      = description;
            AccountNumber    = accountNumber;
            BankAccountType  = bankAccountType;
            InitialBalance   = initialBalance;
            Created          = created;
            Modified         = DateTime.Now;
        }

        public static implicit operator BankAccountDto(BankAccount entity)
            => CreateDto(entity);

        //public static explicit operator BankAccountDto(BankAccount entity)
        // => CreateDto(entity);

        static BankAccountDto CreateDto(BankAccount entity)
            => new BankAccountDto(entity.Id,
                    entity.AccountName.Value,
                    entity.AccountNumber,
                    entity.Description.Value,
                    entity.InitialBalance,
                    entity.BankAccountType?.Id ?? 0L,
                    entity.BankAccountType,
                    entity.Created,
                    entity.Modified);


    }
}
