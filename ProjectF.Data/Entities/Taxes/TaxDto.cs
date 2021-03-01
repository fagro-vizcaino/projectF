using ProjectF.Data.Entities.Core;
using System;

namespace ProjectF.Data.Entities.Taxes
{
    public record TaxDto(string Name
        , decimal PercentValue) : FDto;
}
