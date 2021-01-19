using System;
using ProjectF.Data.Entities.Core;

namespace ProjectF.Data.Entities.PaymentMethods
{
    public record PaymentMethodDto (string Code, 
        string Description, 
        DateTime Created, 
        DateTime? Modified) : FDto;
   
}
