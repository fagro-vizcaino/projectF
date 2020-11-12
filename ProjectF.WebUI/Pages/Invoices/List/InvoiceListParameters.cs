using ProjectF.WebUI.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.WebUI.Pages.Invoices.List
{
    public class InvoiceListParameters : RequestQueryParametersBase
    {
        public DateTime DateFrom { get; set; } = DateTime.Today.AddDays(-30);
        public DateTime DateTo { get; set; } = DateTime.Today;
        
        public override string GetRequestQueryString()
        => $"?pageNumber={PageNumber}&pageSize={PageSize}&dateFrom={DateFrom:yyyy-MM-dd}&dateTo={DateTo:yyyy-MM-dd}";
    }
}
