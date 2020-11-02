using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Services.Common
{
    public abstract class RequestQueryParametersBase
    {
        public int PageTotal { get; set; } 
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } =1;

        public abstract string GetRequestQueryString();

    }
}
