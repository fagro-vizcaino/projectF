using System;
using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Api.Features.Bank
{
    public class BankAccountTypeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }

        public BankAccountTypeDto ToDto()
            => new BankAccountTypeDto(Id, Name, Description, Created, Modified, Status);
        public static BankAccountTypeViewModel FromDto(BankAccountTypeDto bankAccountTypeDto)
            => new BankAccountTypeViewModel()
            {
                Id          = bankAccountTypeDto.Id,
                Name        = bankAccountTypeDto.Name,
                Description = bankAccountTypeDto.Description,
                Created     = bankAccountTypeDto.Created,
                Modified    = bankAccountTypeDto.Modified,
                Status      = bankAccountTypeDto.Status
            };
    }
}