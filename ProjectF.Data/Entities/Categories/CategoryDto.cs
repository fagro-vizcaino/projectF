using System;
using ProjectF.Data.Entities.Common;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.Categories
{
    public record CategoryDto(string Code
        , string Name
        , bool ShowOn
        , DateTime Created
        , DateTime? Modified) : FDto;
}
