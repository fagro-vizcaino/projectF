using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectF.WebUI.Services.Common;

namespace ProjectF.WebUI.Pages.Products
{
    public class ProductListParameters : RequestQueryParametersBase
    {
       public override string GetRequestQueryString()
        => $"?pageNumber={PageNumber}&pageSize={PageSize}";
    
    }
}
