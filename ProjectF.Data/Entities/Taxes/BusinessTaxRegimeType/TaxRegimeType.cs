using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType
{
    public class TaxRegimeType : Entity
    {
        public Name Name { get; private set; }
        public GeneralText Description { get; private set; }
        protected TaxRegimeType() { }
        public TaxRegimeType(Name name, GeneralText description)
        {
            Name = name;
            Description = description;
        }

        public void EditTaxRegimeType(Name name, GeneralText description)
        {
            Name        = name;
            Description = description;
        }

        public static implicit operator TaxRegimeDto(TaxRegimeType entity)
            => new TaxRegimeDto(entity.Id, entity.Name.Value, entity.Description.Value);
    }
}
