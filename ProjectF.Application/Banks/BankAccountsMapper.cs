using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Application.Banks
{
    public static class BankAccountsMapper
    {
        public static BankAccountDto FromEntity(BankAccount entity)
            => new(entity.AccountName.Value,
                entity.AccountNumber,
                entity.Description,
                entity.InitialBalance,
                entity.BankAccountType.Id,
                FromEntity(entity.BankAccountType))
            {
                Id = entity.Id, 
                Created = entity.Created, 
                Status = entity.Status,
                Modified = entity.Modified
            };

        public static BankAccount FromDto(BankAccountDto dto)
            => new(new Name(dto.AccountName),
                dto.AccountNumber,
                new GeneralText(dto.Description),
                dto.InitialBalance,
                FromDto(dto.BankAccountType),
                dto.Created,
                dto.Status);

        public static BankAccountType FromDto(BankAccountTypeDto dto)
            => new(new Name(dto.Name)
                , new GeneralText(dto.Description)
                , dto.Created
                , dto.Status);

        public static BankAccountTypeDto FromEntity(BankAccountType entity)
            => new (entity.Name.Value
                , entity.Description.Value)
            {
                Id = entity.Id,
                Created = entity.Created,
                Modified = entity.Modified,
                Status = entity.Status
            };
    }
}
