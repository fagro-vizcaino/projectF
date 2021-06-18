using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.Banks
{
    public record BankAccountTypeDto( string Name
        , string Description) : FDto;
}
