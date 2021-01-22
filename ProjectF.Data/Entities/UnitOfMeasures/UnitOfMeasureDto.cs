using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.UnitOfMeasures
{
    public record UnitOfMeasureDto(string Name
        , decimal Value
        , DateTime Created
        , DateTime? Modified) : FDto;
}
