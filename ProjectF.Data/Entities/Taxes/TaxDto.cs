using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectF.Data.Entities.Taxes
{
    public record TaxDto(long Id
        , string Name
        , decimal PercentValue
        , long CompanyId
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
