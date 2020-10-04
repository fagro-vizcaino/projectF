using ProjectF.Data.Entities.Banks;
using ProjectF.Data.Entities.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.Banks
{
    public class BankAccountTypeDto
    {
        public long Id { get;  }
        public string Name { get; }
        public string Description { get; }

        public BankAccountTypeDto(long id, string name, string description)
            => (Id, Name, Description) = (id, name?.Trim(), description);
        public void Deconstruct(out long id, out string name, out string description)
            => (id, name, description) = (Id, Name, Description);

        public BankAccountTypeDto With(long? id = null,
            string? name = null,
            string? description = null)
            => new BankAccountTypeDto(id ?? this.Id, 
                name ?? this.Name, 
                description ?? this.Description);


        public static implicit operator BankAccountType(BankAccountTypeDto dto)
            => new BankAccountType(new Name(dto.Name), new GeneralText(dto.Description));
    }
}
