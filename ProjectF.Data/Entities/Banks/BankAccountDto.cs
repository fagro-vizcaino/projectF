using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.Banks
{
    public record BankAccountDto(string AccountName
        , string AccountNumber
        , string? Description
        , decimal InitialBalance
        , long BankAccountTypeId
        , BankAccountTypeDto BankAccountType) : FDto;
}
