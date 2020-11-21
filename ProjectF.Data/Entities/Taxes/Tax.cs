using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Taxes
{
    public class Tax : _BaseEntity
    {
        public Name Name { get; set; }
        public decimal PercentValue { get; set; }

        protected Tax() { }

        public Tax(Name name, decimal value)
        {
            Name         = name;
            PercentValue = value;
        }

        public void EditTax(Name name, decimal value)
        {
            Name         = name;
            PercentValue = value;
        }

        public static implicit operator TaxDto(Tax entity)
            =>  new TaxDto(entity.Id, entity.Name.Value, entity.PercentValue);
    }
}
