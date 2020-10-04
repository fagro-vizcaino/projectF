using ProjectF.Data.Entities.Banks;

namespace ProjectF.Api.Features.Bank
{
    public class BankAccountTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BankAccountTypeDto ToDto()
            => new BankAccountTypeDto(Id, Name, Description);
        public static BankAccountTypeViewModel FromDto(BankAccountTypeDto bankAccountTypeDto)
            => new BankAccountTypeViewModel()
            {
                Id          = bankAccountTypeDto.Id,
                Name        = bankAccountTypeDto.Name,
                Description = bankAccountTypeDto.Description
            };
    }
}