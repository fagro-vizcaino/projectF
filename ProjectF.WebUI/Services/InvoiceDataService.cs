using ProjectF.WebUI.Pages.Invoices;
using ProjectF.WebUI.Pages.Invoices.List;
using System.Net.Http;

namespace ProjectF.WebUI.Services
{
    public class InvoiceDataService : BaseDataService<Invoice>
    {
        const string baseUrl = "sales/invoice";

        public InvoiceDataService(HttpClient client) 
            : base(baseUrl, client) { }
    }

    public class InvoiceListDataService : BaseDataService<InvoiceMainList>
    {
        const string baseUrl = "sales/invoice";

        public InvoiceListDataService(HttpClient client)
            : base(baseUrl, client) { }
    }
}
