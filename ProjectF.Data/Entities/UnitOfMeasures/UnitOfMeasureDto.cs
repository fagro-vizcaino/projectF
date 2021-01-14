using System;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Entities.UnitOfMeasures
{
    public record UnitOfMeasureDto(long Id
        , string Name
        , decimal Value
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
