using System;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Entities.Categories
{
    public record CategoryDto(long Id
        , string Code
        , string Name
        , bool ShowOn
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
