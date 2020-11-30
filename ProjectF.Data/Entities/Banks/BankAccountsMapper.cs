using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Banks
{
    public static class BankAccountsMapper
    {
        public static BankAccount FromDto(BankAccountDto dto)
            => new BankAccount(new Name(dto.AccountName),
                dto.AccountNumber,
                new GeneralText(dto.Description),
                dto.InitialBalance,
                dto.BankAccountType,
                dto.Created,
                dto.Status);
        public static BankAccountDto FromEntity(BankAccount entity)
            => new BankAccountDto(entity.Id
                , entity.AccountName.Value
                , entity.AccountNumber
                , entity.Description.Value
                , entity.InitialBalance
                , entity.BankAccountType.Id
                , entity.BankAccountType
                , entity.Created
                , entity.Modified
                , entity.Status);



        public static BankAccountType FromDto(BankAccountTypeDto dto)
            => new BankAccountType(new Name(dto.Name)
                , new GeneralText(dto.Description)
                , dto.Created
                , dto.Status);


        public static BankAccountTypeDto FromEntity(BankAccountType entity)
            => new BankAccountTypeDto(entity.Id
                , entity.Name.Value
                , entity.Description.Value
                , entity.Created
                , entity.Modified
                , entity.Status);
    }
}
