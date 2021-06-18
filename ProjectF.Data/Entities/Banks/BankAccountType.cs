using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using LanguageExt;
using ProjectF.Data.Entities.Banks;
using System;

namespace ProjectF.Data.Entities.Banks
{
    public class BankAccountType : _BaseEntity
    {
        public Name Name { get; private set; }
        public GeneralText Description { get; private set; }

        public BankAccountType(Name name, 
            GeneralText description, 
            DateTime created,
            EntityStatus status = EntityStatus.Active)
            => (Name, Description, Created, Status) 
            =  (name, description, created == DateTime.MinValue ? DateTime.Now : created, status);

        public void EditBankAccountType(
            Name name,
            GeneralText description,
            EntityStatus status)
            => (Name, Description, Status) 
            = (name, description, status);

        public void Deconstruct(out long id, 
            out Name name, 
            out GeneralText description,
            out DateTime created,
            out DateTime? modified,
            out EntityStatus status)
            => (id, name, description, created, modified, status) 
            = (Id, Name, Description, Created, Modified, Status);
    }
}
