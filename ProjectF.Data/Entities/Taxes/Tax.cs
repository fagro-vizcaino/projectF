using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.Taxes
{
    public class Tax : _BaseEntity
    {
        public Name Name { get; set; }
        public decimal PercentValue { get; set; }

        protected Tax() { }

        public Tax(Name name, decimal value,
            DateTime created,
            DateTime? modified = null,
            int companyId = 0,
            EntityStatus status = EntityStatus.Active)
        {
            Name         = name;
            PercentValue = value;
            Created      = created == DateTime.MinValue ? DateTime.Now : created;
            Modified     = modified;
            CompanyId    = companyId;
            Status       = status;
        }

        public void EditTax(Name name, decimal value, EntityStatus status)
        {
            Name         = name;
            PercentValue = value;
            Status       = status;
            Modified     = DateTime.Now;
        }
    }
}
