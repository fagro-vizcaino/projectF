using ProjectF.Data.Entities.Common;
using System;

namespace ProjectF.Data.Entities.Banks
{
    public record BankAccountDto(long Id
        , string AccountName
        , string AccountNumber
        , string Description
        , decimal InitialBalance
        , long BankAccountTypeId
        , BankAccountType BankAccountType
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
