using System;
using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Api.Features.Bank
{
    public class BankAccountTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public EntityStatus Status { get; set; }

        public BankAccountTypeDto ToDto()
            => new( Name, Description)
            {
                Id = Id,
                Created = Created,
                Modified = Modified,
                Status = Status
            };
    }
}