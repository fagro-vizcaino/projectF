using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Entities.PaymentMethods
{
    public record PaymentMethodDto (long Id, 
        string Code, 
        string Description, 
        DateTime Created, 
        DateTime? Modified, 
        EntityStatus Status);
   
}
