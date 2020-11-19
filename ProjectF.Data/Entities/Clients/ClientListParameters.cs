using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectF.Data.Entities.RequestFeatures;

namespace ProjectF.Data.Entities.Clients
{
    public class ClientListParameters : RequestParameters
    {

    }

    public enum ClientStatus
    {
        BalanceDue,
        IsActive
    }
}
