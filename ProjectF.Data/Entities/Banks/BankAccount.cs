﻿using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.Banks
{
    public class BankAccount : _BaseEntity
    {
        public Name AccountName { get; private set; }
        public string AccountNumber { get; private set; }
        public GeneralText Description { get; private set; }
        public decimal InitialBalance { get; private set; }
        public virtual BankAccountType BankAccountType { get; private set; }

        protected BankAccount() { }

        public BankAccount(Name accountName,
            string accountNumber,
            GeneralText description,
            decimal initialBalance,
            BankAccountType bankAccountType,
            DateTime created,
            EntityStatus status = EntityStatus.Active)
        {
            AccountName      = accountName;
            AccountNumber    = accountNumber;
            Description      = description;
            InitialBalance   = initialBalance;
            AccountNumber    = accountNumber;
            BankAccountType  = bankAccountType;
            Created          = created == DateTime.MinValue ? DateTime.Now : created;
            Status           = status;
        }

        public void EditBank(Name accountName,
            string accountNumber,
            GeneralText description,
            decimal initialBalance,
            BankAccountType bankAccountType,
            EntityStatus status)
        {
            AccountName      = accountName;
            AccountNumber    = accountNumber;
            Description      = description;
            AccountNumber    = accountNumber;
            BankAccountType  = bankAccountType;
            InitialBalance   = initialBalance;
            Modified         = DateTime.Now;
            Status           = status;
        }
    }
}
