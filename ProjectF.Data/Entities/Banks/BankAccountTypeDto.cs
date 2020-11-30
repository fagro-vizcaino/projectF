using ProjectF.Data.Entities.Common;
using System;

namespace ProjectF.Data.Entities.Banks
{
    public record BankAccountTypeDto(long Id
        , string Name
        , string Description
        , DateTime Created
        , DateTime? Modified
        , EntityStatus Status);
}
