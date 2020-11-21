using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using LanguageExt;
using ProjectF.Data.Entities.Banks;

namespace ProjectF.Data.Entities.Banks
{
    public class BankAccountType : _BaseEntity
    {
        public Name Name { get; private set; }
        public GeneralText Description { get; private set; }

        public BankAccountType(Name name,
            GeneralText description)
            => (Name, Description) = (name, description);

        public void EditBankAccountType(
            Name name,
            GeneralText description)
            => (Name, Description) = (name, description);

        public void Deconstruct(out long id, out Name name, out GeneralText description)
            => (id, name, description) = (Id, Name, Description);


        public static implicit operator BankAccountTypeDto(BankAccountType entity)
            => new BankAccountTypeDto(entity.Id, entity.Name.Value, entity.Description.Value);

    }
}
